using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Shengtai.IdentityServer.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Service
{
    public class SignInService<TUser> : ISignInService where TUser : ApplicationUser
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly IUserService _userService;

        public SignInService(SignInManager<TUser> signInManager, IUserService userService)
        {
            _signInManager = signInManager;
            _userService = userService;
        }

        public Task<AuthenticationProperties> ConfigureExternalAuthenticationPropertiesAsync(string provider, string redirectUrl, string userId = null)
        {
            var result = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl, userId);

            return Task.FromResult(result);
        }

        public Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor)
        {
            return _signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent, bypassTwoFactor);
        }

        public Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        public Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null)
        {
            return _signInManager.GetExternalLoginInfoAsync(expectedXsrf);
        }

        public Task<bool> IsSignedInAsync(ClaimsPrincipal principal)
        {
            var result = _signInManager.IsSignedIn(principal);

            return Task.FromResult(result);
        }

        public async Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var identityUser = await _userService.ChangeTypeAsync<TUser>(user);
            var result = await _signInManager.PasswordSignInAsync(identityUser as TUser, password, isPersistent, lockoutOnFailure);
            return result;
        }

        public async Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null)
        {
            var identityUser = await _userService.ChangeTypeAsync<TUser>(user);
            await _signInManager.SignInAsync(identityUser as TUser, isPersistent, authenticationMethod);
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }
    }
}
