using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Shengtai.Web;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shengtai.Core
{
    public static partial class Extensions
    {
        //public static async Task<SignInResult> PasswordSignInAsync<TUser>(this IAccountService<TUser> service,
        //    UserManager<TUser> userManager, SignInManager<TUser> signInManager,
        //    string account, string password, bool isPersistent, bool lockoutOnFailure) where TUser : IdentityUser
        //{
        public static async Task<SignInResult> PasswordSignInAsync<TUser>(this IAccountService service,
            UserManager<TUser> userManager, SignInManager<TUser> signInManager,
            string account, string password, bool isPersistent, bool lockoutOnFailure) where TUser : IdentityUser
        {
            if (string.IsNullOrEmpty(account))
                throw new ArgumentNullException("account");

            string userId = await service.FindIdByAccountAsync(account);
            TUser user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return SignInResult.Failed;

            SignInResult result = await signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
            if (result.Succeeded)
            {
                bool flag = userManager.SupportsUserTwoFactor;
                if (flag)
                {
                    flag = await userManager.GetTwoFactorEnabledAsync(user);
                    if (flag)
                    {
                        flag = (await userManager.GetValidTwoFactorProvidersAsync(user)).Count > 0;
                    }
                }

                if (flag && !(await signInManager.IsTwoFactorClientRememberedAsync(user)))
                {
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(IdentityConstants.TwoFactorUserIdScheme);
                    claimsIdentity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", userId));

                    await AuthenticationHttpContextExtensions.SignInAsync(signInManager.Context, IdentityConstants.TwoFactorUserIdScheme, new ClaimsPrincipal(claimsIdentity));

                    return SignInResult.TwoFactorRequired;
                }

                await signInManager.SignInAsync(user, isPersistent, null);
                return SignInResult.Success;
            }

            return result;
        }
    }
}