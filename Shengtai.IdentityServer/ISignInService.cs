using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public interface ISignInService
    {
        bool IsSignedIn(ClaimsPrincipal principal);
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
        Task<SignInResult> PasswordSignInAsync(Models.Account.ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure);
        Task SignInAsync(Models.Account.ApplicationUser user, bool isPersistent, string authenticationMethod = null);
        Task SignOutAsync();
    }
}
