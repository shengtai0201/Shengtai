using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Shengtai.Web;
using System;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Shengtai.Net
{
    public static partial class Extensions
    {
        #region PasswordSignInAsync

        private static async Task<bool> IsTwoFactorEnabledAsync<TUser>(UserManager<TUser> userManager, string userId) where TUser : IdentityUser
        {
            var result = await userManager.GetTwoFactorEnabledAsync(userId);
            if (result)
                result = (await userManager.GetValidTwoFactorProvidersAsync(userId)).Count > 0;

            return result;
        }

        public static async Task<SignInStatus> PasswordSignInAsync<TUser>(this IAccountService service,
            UserManager<TUser> userManager, IAuthenticationManager authenticationManager, string account, string password,
            bool isPersistent, bool shouldLockout) where TUser : IdentityUser
        {
            if (userManager == null || authenticationManager == null)
                return SignInStatus.Failure;

            var user = await FindByAccountAsync(service, userManager, account);
            if (user == null)
                return SignInStatus.Failure;

            if (await userManager.IsLockedOutAsync(user.Id))
                return SignInStatus.LockedOut;

            if (await userManager.CheckPasswordAsync(user, password))
            {
                var result = false;
                var values = ConfigurationManager.AppSettings.GetValues("Microsoft.AspNet.Identity.PasswordSignInAlwaysResetLockoutOnSuccess");
                if (values != null)
                    bool.TryParse(values.FirstOrDefault(), out result);

                if (!result)
                    result = !(await IsTwoFactorEnabledAsync(userManager, user.Id));

                if (result)
                    await userManager.ResetAccessFailedCountAsync(user.Id);

                return await SignInOrTwoFactor(userManager, authenticationManager, user, isPersistent);
            }

            if (shouldLockout)
            {
                await userManager.AccessFailedAsync(user.Id);
                if (await userManager.IsLockedOutAsync(user.Id))
                    return SignInStatus.LockedOut;
            }

            return SignInStatus.Failure;
        }

        public static async Task<bool> UpdateSecurityStampAsync<TUser>(this IAccountService service, UserManager<TUser> userManager, string account, string password)
            where TUser : IdentityUser
        {
            var user = await FindByAccountAsync(service, userManager, account);
            if (user != null)
            {
                bool result = await userManager.IsLockedOutAsync(user.Id);
                if (!result)
                {
                    result = await userManager.CheckPasswordAsync(user, password);
                    if (result)
                    {
                        // change the security stamp only on correct username/password
                        var identityResult = await userManager.UpdateSecurityStampAsync(user.Id);
                        return identityResult.Succeeded;
                    }
                }
            }

            return false;
        }

        private static async Task SignInAsync<TUser>(UserManager<TUser> userManager,
            IAuthenticationManager authenticationManager, TUser user, bool isPersistent, bool rememberBrowser)
            where TUser : IdentityUser
        {
            var userIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            authenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie, DefaultAuthenticationTypes.TwoFactorCookie);

            AuthenticationProperties properties = new AuthenticationProperties { IsPersistent = isPersistent };

            if (rememberBrowser)
            {
                ClaimsIdentity claimsIdentity = authenticationManager.CreateTwoFactorRememberBrowserIdentity(user.Id);
                authenticationManager.SignIn(properties, userIdentity, claimsIdentity);
            }
            else
                authenticationManager.SignIn(properties, userIdentity);
        }

        private static async Task<TUser> FindByAccountAsync<TUser>(IAccountService service, UserManager<TUser> userManager, string account) where TUser : IdentityUser
        {
            if (string.IsNullOrEmpty(account))
                throw new ArgumentNullException("account");

            string userId = await service.FindIdByAccountAsync(account);
            var user = await userManager.FindByIdAsync(userId);
            return user;
        }

        private static async Task<SignInStatus> SignInOrTwoFactor<TUser>(UserManager<TUser> userManager,
            IAuthenticationManager authenticationManager, TUser user, bool isPersistent) where TUser : IdentityUser
        {
            var result = await IsTwoFactorEnabledAsync(userManager, user.Id);
            if (result)
                result = !(await authenticationManager.TwoFactorBrowserRememberedAsync(user.Id));

            if (result)
            {
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
                claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));

                authenticationManager.SignIn(claimsIdentity);

                return SignInStatus.RequiresVerification;
            }

            await SignInAsync(userManager, authenticationManager, user, isPersistent, false);
            return SignInStatus.Success;
        }

        #endregion PasswordSignInAsync

        #region ExternalSignInAsync

        public static async Task<SignInStatus> SignInAsync<TUser>(this IAuthenticationManager authenticationManager, UserManager<TUser> userManager, bool isPersistent, UserLoginInfo login) where TUser : IdentityUser
        {
            var user = await userManager.FindAsync(login);
            if (user == null)
                return SignInStatus.Failure;

            if (await userManager.IsLockedOutAsync(user.Id))
                return SignInStatus.LockedOut;

            return await SignInOrTwoFactor(userManager, authenticationManager, user, isPersistent);
        }

        #endregion ExternalSignInAsync
    }
}