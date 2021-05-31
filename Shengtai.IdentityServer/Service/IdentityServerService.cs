using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shengtai.IdentityServer.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Service
{
    public class IdentityServerService<TUser> : IEmailService, IIdentityServerService where TUser : ApplicationUser
    {
        private readonly ILogger<IdentityServerService<TUser>> _logger;
        private readonly IAppSettings _appSettings;
        private readonly IUserService _userService;
        private readonly ConfigurationDbContext _configurationDbContext;

        public IdentityServerService(ILogger<IdentityServerService<TUser>> logger, IAppSettings appSettings, IUserService userService,
            ConfigurationDbContext configurationDbContext)
        {
            _logger = logger;
            _appSettings = appSettings;
            _userService = userService;
            _configurationDbContext = configurationDbContext;
        }

        public async Task<(string ClientId, string ClientSecret)> AddClientAsync(ApplicationUser user)
        {
            (string ClientId, string ClientSecret) result = (user.Account + "-" + Security.Membership.GeneratePassword(4, 4), Security.Membership.GeneratePassword(8, 1));

            var identityResult = await _userService.AddClaimAsync(user, _appSettings.IdentityServer.Configuration.ClientIdClaimType, result.ClientId);
            if (identityResult.Succeeded)
            {
                identityResult = await _userService.AddClaimAsync(user, _appSettings.IdentityServer.Configuration.ClientSecretClaimType, result.ClientSecret);
                if (identityResult.Succeeded)
                {
                    var client = new Client
                    {
                        ClientId = result.ClientId,
                        ClientSecrets = { new Secret(result.ClientSecret.Sha256()) },
                        AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                        AllowedScopes =
                        {
                            _appSettings.IdentityServer.Configuration.ApiScopeName,
                            IdentityServer4.IdentityServerConstants.StandardScopes.OpenId,
                            IdentityServer4.IdentityServerConstants.StandardScopes.Profile,
                            IdentityServer4.IdentityServerConstants.StandardScopes.Email
                        },
                        RequirePkce = true,
                        RequireConsent = false,
                        AllowOfflineAccess = true,
                        RedirectUris = { $"{_appSettings.IdentityServer.Configuration.Uri}/signin-oidc" },
                        PostLogoutRedirectUris = { $"{_appSettings.IdentityServer.Configuration.Uri}/signout-callback-oidc" }
                    }.ToEntity();

                    await _configurationDbContext.Clients.AddAsync(client);
                    try
                    {
                        var identityUser = await _userService.ChangeTypeAsync<TUser>(user);
                        await _userService.AddToRolesAsync(identityUser as TUser, _appSettings.IdentityServer.Roles);

                        await _configurationDbContext.SaveChangesAsync();

                        await this.SendEmailAsync(user.Email, "API 帳密", $"<p>Your ClientId:{result.ClientId}</p><br /><p>Your ClientSecret:{result.ClientSecret}</p><br /><p>Your Scope:{_appSettings.IdentityServer.Configuration.ApiScopeName}</p>");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);

                        result.ClientId = null;
                        result.ClientSecret = null;
                    }
                }
                else
                {
                    result.ClientId = null;
                    result.ClientSecret = null;
                }
            }
            else
            {
                result.ClientId = null;
                result.ClientSecret = null;
            }

            return result;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await this.SendMailAsync(subject, htmlMessage, email);
        }

        public async Task<bool> SendMailAsync(string subject, string body, params string[] toAddress)
        {
            if (toAddress == null)
                return false;

            var address = string.Join(", ", toAddress);
            var message = new MailMessage(new MailAddress(_appSettings.IdentityServer.Email.From, _appSettings.IdentityServer.ApplicationName), new MailAddress(address))
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            bool result = false;
            using (var client = new SmtpClient(_appSettings.IdentityServer.Email.Host, _appSettings.IdentityServer.Email.Port)
            {
                Credentials = new NetworkCredential(_appSettings.IdentityServer.Email.UserName, _appSettings.IdentityServer.Email.Password),
                EnableSsl = _appSettings.IdentityServer.Email.EnableSsl
            })
            {
                try
                {
                    await client.SendMailAsync(message);
                    result = true;
                }
                catch (Exception e)
                {
                    _logger.LogError(e.Message);
                }
            }

            return result;
        }
    }
}
