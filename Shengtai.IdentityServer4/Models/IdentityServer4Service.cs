using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer4.Models
{
    public class IdentityServer4Service<TUser> : IIdentityServer4Service where TUser : IdentityUser
    {
        private readonly SignInManager<TUser> _signInManager;

        public IdentityServer4Service(SignInManager<TUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public bool IsSignedIn(ClaimsPrincipal principal)
        {
            return _signInManager.IsSignedIn(principal);
        }
    }
}
