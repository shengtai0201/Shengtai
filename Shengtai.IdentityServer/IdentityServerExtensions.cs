using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using PwnedPasswords.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public static class IdentityServerExtensions
    {
        private class CustomErrorDescriber : PwnedPasswordErrorDescriber
        {
            public override IdentityError PwnedPassword()
            {
                return new IdentityError
                {
                    Code = nameof(PwnedPassword),
                    Description = "The password you entered has appeared in a data breach. Please choose a different password."
                };
            }
        }

        public static void AddIdentityServer<TUser>(this IServiceCollection services, string connectionString, string assemblyName) where TUser : Models.Account.ApplicationUser
        {
            // AutoMapper
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Models.Account.ApplicationUser, TUser>());

            #region default service
            services.AddSingleton(provider => config.CreateMapper());
            services.AddScoped<ISignInService, Service.SignInService<TUser>>();
            services.AddScoped<IUserService, Service.UserService<TUser>>();
            services.AddScoped<IRoleService, Service.RoleService>();
            services.AddScoped<Service.IdentityServerService<TUser>>();
            services.AddScoped<IEmailService>(x => x.GetRequiredService<Service.IdentityServerService<TUser>>());
            #endregion

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            }).AddConfigurationStore(options =>     // this adds the config data from DB (clients, resources, CORS)
            {
                options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString, builder => builder.MigrationsAssembly(assemblyName));
            }).AddOperationalStore(options =>       // this adds the operational data from DB (codes, tokens, consents)
            {
                options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString, builder => builder.MigrationsAssembly(assemblyName));

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
            }).AddAspNetIdentity<TUser>();

            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
        }

        public static void AddIdentityServer<TUser, TDataStrategy, TMenuBuilder>(this IServiceCollection services, IdentityBuilder builder, string connectionString, string assemblyName) 
            where TUser : Models.Account.ApplicationUser
            where TDataStrategy : class, Data.IDataStrategy
            where TMenuBuilder : MenuBuilder
        {
            // AutoMapper
            var config = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<Models.Account.ApplicationUser, TUser>());

            #region default service
            services.AddSingleton(provider => config.CreateMapper());
            services.AddScoped<ISignInService, Service.SignInService<TUser>>();
            services.AddScoped<IUserService, Service.UserService<TUser>>();
            services.AddScoped<IRoleService, Service.RoleService>();
            services.AddScoped<Service.IdentityServerService<TUser>>();
            services.AddScoped<IEmailService>(x => x.GetRequiredService<Service.IdentityServerService<TUser>>());
            services.AddScoped<IIdentityServerService>(x => x.GetRequiredService<Service.IdentityServerService<TUser>>());
            services.AddScoped<Data.IDataStrategy, TDataStrategy>();
            services.AddScoped<MenuBuilder, TMenuBuilder>();
            #endregion

            #region 密碼
            /* 額外裝的套件
             * Microsoft.Extensions.Http.Polly
             * Polly
             * PwnedPasswords.Validator
             */
            // Explicitly configure the PwnedPassword client to timeout after 2 seconds, and retry 3 times
            // The pwnedpassword API achieves 99% percentile of <1s, so this should be sufficient!
            services.AddPwnedPasswordHttpClient(minimumFrequencyToConsiderPwned: 20)
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(3))
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(2)));

            builder.AddPwnedPasswordValidator<TUser>().AddPwnedPasswordErrorDescriber<CustomErrorDescriber>();
            #endregion

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.UseMemberCasing();
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/IdentityServer/Account/Login";
                options.LogoutPath = "/IdentityServer/Account/Logout";
                options.AccessDeniedPath = "/IdentityServer/Account/AccessDenied";
            });

            var serverBuilder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            }).AddConfigurationStore(options =>     // this adds the config data from DB (clients, resources, CORS)
            {
                options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString, builder => builder.MigrationsAssembly(assemblyName));
            }).AddOperationalStore(options =>       // this adds the operational data from DB (codes, tokens, consents)
            {
                options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString, builder => builder.MigrationsAssembly(assemblyName));

                // this enables automatic token cleanup. this is optional.
                options.EnableTokenCleanup = true;
            }).AddAspNetIdentity<TUser>();

            // not recommended for production - you need to store your key material somewhere secure
            serverBuilder.AddDeveloperSigningCredential();

            services.AddSignalR();
        }

        /// <summary>
        /// Checks if the redirect URI is for a native client.
        /// </summary>
        /// <param name="context">Represents contextual information about a authorization request.</param>
        /// <returns></returns>
        public static bool IsNativeClient(this IdentityServer4.Models.AuthorizationRequest context)
        {
            return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal) && !context.RedirectUri.StartsWith("http", StringComparison.Ordinal);
        }

        public static void LoadingPage(this PageModel model, string actionName, string redirectUrl)
        {
            model.HttpContext.Response.StatusCode = 200;
            model.HttpContext.Response.Headers["Location"] = string.Empty;

            model.RedirectToAction(actionName, new Models.Account.RedirectViewModel { RedirectUrl = redirectUrl });
        }

        public static IActionResult LoadingPage(this Controller controller, string viewName, string redirectUri)
        {
            controller.HttpContext.Response.StatusCode = 200;
            controller.HttpContext.Response.Headers["Location"] = "";

            return controller.View(viewName, new Models.Account.RedirectViewModel { RedirectUrl = redirectUri });
        }

        public static async Task<Models.Account.ApplicationUser> ChangeTypeAsync<TUser>(this IUserService userService, Models.Account.ApplicationUser user) where TUser : Models.Account.ApplicationUser
        {
            if (user.GetType() == typeof(TUser))
                return user;

            return await userService.FindByIdAsync(user.Id);
        }
    }
}
