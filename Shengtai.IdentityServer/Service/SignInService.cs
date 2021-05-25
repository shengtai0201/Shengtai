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
        private readonly AutoMapper.IMapper _mapper;
        private readonly SignInManager<TUser> _signInManager;

        public SignInService(AutoMapper.IMapper mapper, SignInManager<TUser> signInManager)
        {
            _mapper = mapper;
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
            return _signInManager.PasswordSignInAsync(_mapper.ChangeType<ApplicationUser, TUser>(user), password, isPersistent, lockoutOnFailure);
        }

        public Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null)
        {
            return _signInManager.SignInAsync(_mapper.ChangeType<ApplicationUser, TUser>(user), isPersistent, authenticationMethod);
        }

        public Task SignOutAsync()
        {
            return _signInManager.SignOutAsync();
        }
    }
}
