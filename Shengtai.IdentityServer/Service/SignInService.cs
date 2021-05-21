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

        public SignInService(SignInManager<TUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            return _signInManager.GetExternalAuthenticationSchemesAsync();
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return _signInManager.IsSignedIn(principal);
        }

        public Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return _signInManager.PasswordSignInAsync(user as TUser, password, isPersistent, lockoutOnFailure);
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
