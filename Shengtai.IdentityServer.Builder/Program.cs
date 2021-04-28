using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Shengtai.IdentityServer.Builder
{
    class Program
    {
        private static bool AddIdentityResource<T>(ConfigurationDbContext dbContext, T identityResource) where T : IdentityResource
        {
            var entity = identityResource.ToEntity();
            var exist = dbContext.IdentityResources.FromSqlRaw($"SELECT * FROM \"IdentityResources\" i WHERE i.\"Name\" = '{entity.Name}'").Count();
            if (exist == 0)
            {
                dbContext.IdentityResources.Add(entity);
                return true;
            }

            return false;
        }

        static void Main(string[] args)
        {
            #region 設定
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).AddEnvironmentVariables().AddCommandLine(args).Build();
            var services = new ServiceCollection().AddLogging(builder => builder.AddConsole(options => options.LogToStandardErrorThreshold = LogLevel.Information));

            var appSettings = AppSettings.AddSingleton(services, configuration, settings => services.AddSingleton<IAppSettings>(settings));
            var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(appSettings.ConnectionStrings.DefaultConnection));
            services.AddDefaultIdentity<Models.Account.ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer<Models.Account.ApplicationUser>(appSettings.ConnectionStrings.DefaultConnection, assemblyName);
            #endregion

            using (var provider = services.BuildServiceProvider())
            {
                using (var scope = provider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var logger = provider.GetService<ILoggerFactory>().CreateLogger<Program>();
                    logger.LogInformation("Starting application");

                    var dbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                    bool anyChanges = AddIdentityResource(dbContext, new IdentityResources.OpenId());
                    anyChanges |= AddIdentityResource(dbContext, new IdentityResources.Profile());
                    anyChanges |= AddIdentityResource(dbContext, new IdentityResources.Email());
                    anyChanges |= AddIdentityResource(dbContext, new IdentityResources.Phone());
                    anyChanges |= AddIdentityResource(dbContext, new IdentityResources.Address());
                    if (anyChanges)
                        dbContext.SaveChanges();

                    var apiScope = new ApiScope(appSettings.IdentityServer.Configuration.ApiScopeName, appSettings.IdentityServer.Configuration.ApiScopeDisplayName).ToEntity();
                    var apiScopeEntity = dbContext.ApiScopes.FromSqlRaw($"SELECT * FROM \"ApiScopes\" a WHERE a.\"Name\" = '{apiScope.Name}'").Count();
                    if (apiScopeEntity == 0)
                    {
                        dbContext.ApiScopes.Add(apiScope);
                        dbContext.SaveChanges();
                    }

                    var client = new Client
                    {
                        ClientId = appSettings.IdentityServer.Configuration.ClientId,
                        ClientSecrets = { new Secret(appSettings.IdentityServer.Configuration.SecretValue.Sha256()) },
                        AllowedGrantTypes = GrantTypes.Code,
                        RedirectUris = { $"{appSettings.IdentityServer.Configuration.Uri}/signin-oidc" },
                        PostLogoutRedirectUris = { $"{appSettings.IdentityServer.Configuration.Uri}/signout-callback-oidc" },
                        AllowedScopes = new List<string>
                        {
                            IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServerConstants.StandardScopes.Profile,
                            appSettings.IdentityServer.Configuration.ApiScopeName
                        }
                    }.ToEntity();
                    var clientEntity = dbContext.Clients.FromSqlRaw($"SELECT * FROM \"Clients\" c WHERE c.\"ClientId\" = '{client.ClientId}'").Count();
                    if (clientEntity == 0)
                    {
                        dbContext.Clients.Add(client);
                        dbContext.SaveChanges();
                    }

                    logger.LogInformation("All done!");
                }
            }
        }
    }
}
