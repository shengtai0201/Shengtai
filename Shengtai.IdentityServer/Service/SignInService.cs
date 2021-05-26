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

        public Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return _signInManager.IsSignedIn(principal);
        }

        public async Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            var identityUser = await _userService.ChangeTypeAsync<TUser>(user);
            return await _signInManager.PasswordSignInAsync(identityUser as TUser, password, isPersistent, lockoutOnFailure);
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
