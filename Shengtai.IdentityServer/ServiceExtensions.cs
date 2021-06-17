using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Net.Http;

namespace Shengtai.IdentityServer
{
    public static class ServiceExtensions
    {
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

        public static void AddIdentityServer<TUser, TDataStrategy, TMenuBuilder>(this IServiceCollection services, IdentityBuilder builder, IAppSettings appSettings, string connectionString, string assemblyName)
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
            services.AddScoped<IEmailService>(provider => provider.GetService<Service.IdentityServerService<TUser>>());
            services.AddScoped<IIdentityServerService>(provider => provider.GetService<Service.IdentityServerService<TUser>>());
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

            builder.AddPwnedPasswordValidator<TUser>().AddPwnedPasswordErrorDescriber<DefaultErrorDescriber>();
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

            // external provider authentication
            if (appSettings.Authentication != null)
            {
                var authenticationBuilder = services.AddAuthentication();

                // Google
                if (appSettings.Authentication.Google != null)
                {
                    authenticationBuilder = authenticationBuilder.AddGoogle(options =>
                    {
                        options.ClientId = appSettings.Authentication.Google.ClientId;
                        options.ClientSecret = appSettings.Authentication.Google.ClientSecret;
                    });
                }

                // Facebook
                if(appSettings.Authentication.Facebook != null)
                {
                    authenticationBuilder = authenticationBuilder.AddFacebook(options =>
                    {
                        options.AppId = appSettings.Authentication.Facebook.AppId;
                        options.AppSecret = appSettings.Authentication.Facebook.AppSecret;
                    });
                }

                // Microsoft
                if(appSettings.Authentication.Microsoft != null)
                {
                    authenticationBuilder = authenticationBuilder.AddMicrosoftAccount(options =>
                    {
                        options.ClientId = appSettings.Authentication.Microsoft.ClientId;
                        options.ClientSecret = appSettings.Authentication.Microsoft.ClientSecret;
                    });
                }

                // Twitter
                if(appSettings.Authentication.Twitter != null)
                {
                    authenticationBuilder = authenticationBuilder.AddTwitter(options =>
                    {
                        options.ConsumerKey = appSettings.Authentication.Twitter.ConsumerAPIKey;
                        options.ConsumerSecret = appSettings.Authentication.Twitter.ConsumerSecret;
                        options.RetrieveUserDetails = true;
                    });
                }
            }

            // not recommended for production - you need to store your key material somewhere secure
            serverBuilder.AddDeveloperSigningCredential();

            // todo: check
            services.AddRazorPages();
            services.AddSignalR();
        }
    }
}
