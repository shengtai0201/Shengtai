using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Authentication;
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
        private readonly AutoMapper.IMapper _mapper;
        private readonly ILogger<IdentityServerService<TUser>> _logger;
        private readonly IAppSettings _appSettings;
        private readonly IUserService _userService;
        private readonly IdentityServer4.EntityFramework.DbContexts.ConfigurationDbContext _configurationDbContext;

        public IdentityServerService(AutoMapper.IMapper mapper, ILogger<IdentityServerService<TUser>> logger, IAppSettings appSettings, IUserService userService, IdentityServer4.EntityFramework.DbContexts.ConfigurationDbContext configurationDbContext)
        {
            _mapper = mapper;
            _logger = logger;
            _appSettings = appSettings;
            _userService = userService;
            _configurationDbContext = configurationDbContext;
        }

        public async Task<(string ClientId, string ClientSecret, string Scope)> AddClientAsync(ApplicationUser user)
        {
            (string ClientId, string ClientSecret, string Scope) result = (user.Account + "-" + Security.Membership.GeneratePassword(4, 4),
                Security.Membership.GeneratePassword(8, 1), _appSettings.IdentityServer.Configuration.ApiScopeName);

            var client = new Client
            {
                ClientId = result.ClientId,
                ClientSecrets = { new Secret(result.ClientSecret.Sha256()) },
                //AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                AllowedScopes = { result.Scope },
                // todo: 以下新增, 有待測試
                RequirePkce = true,
                RequireConsent = false,
                AllowOfflineAccess = true,
                RedirectUris = { $"{_appSettings.IdentityServer.Configuration.Uri}/signin-oidc" },
                PostLogoutRedirectUris = { $"{_appSettings.IdentityServer.Configuration.Uri}/signout-callback-oidc" }
            }.ToEntity();

            await _configurationDbContext.Clients.AddAsync(client);
            try
            {
                await _userService.AddToRolesAsync(_mapper.ChangeType<ApplicationUser, TUser>(user), _appSettings.IdentityServer.Roles);

                await _configurationDbContext.SaveChangesAsync();

                await this.SendEmailAsync(user.Email, "API 帳密", $"<p>Your ClientId:{result.ClientId}</p><br /><p>Your ClientSecret:{result.ClientSecret}</p><br /><p>Your Scope:{result.Scope}</p>");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                result = (null, null, null);
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
