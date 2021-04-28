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
    public class IdentityServerService<TUser> : ISignInService, IUserService, IEmailService where TUser : ApplicationUser
    {
        private readonly ILogger<IdentityServerService<TUser>> _logger;
        private readonly IAppSettings _appSettings;
        private readonly SignInManager<TUser> _signInManager;
        private readonly UserManager<TUser> _userManager;

        public IdentityServerService(ILogger<IdentityServerService<TUser>> logger, IAppSettings appSettings, SignInManager<TUser> signInManager, UserManager<TUser> userManager)
        {
            _logger = logger;
            _appSettings = appSettings;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public bool RequireConfirmedAccount => _userManager.Options.SignIn.RequireConfirmedAccount;

        public Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            return _userManager.ConfirmEmailAsync(user as TUser, token);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return _userManager.CreateAsync(user as TUser, password);
        }

        public async Task<ApplicationUser> FindByAccountAsync(string account)
        {
            return await _userManager.Users.SingleOrDefaultAsync(u => u.Account == account);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(user as TUser);
        }

        public Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        public Task<string> GetUserIdAsync(ApplicationUser user)
        {
            return _userManager.GetUserIdAsync(user as TUser);
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return _signInManager.IsSignedIn(principal);
        }

        public Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return _signInManager.PasswordSignInAsync(user as TUser, password, isPersistent, lockoutOnFailure);
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

        public Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null)
        {
            return _signInManager.SignInAsync(user as TUser, isPersistent, authenticationMethod);
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }
    }
}
