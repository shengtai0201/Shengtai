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
        /// <summary>
        /// Returns true if the principal has an identity with the application cookie identity
        /// </summary>
        /// <param name="principal">The System.Security.Claims.ClaimsPrincipal instance.</param>
        /// <returns>True if the user is logged in with identity.</returns>
        Task<bool> IsSignedInAsync(ClaimsPrincipal principal);

        /// <summary>
        /// Gets a collection of Microsoft.AspNetCore.Authentication.AuthenticationSchemes for the known external login providers.
        /// </summary>
        /// <returns>A collection of Microsoft.AspNetCore.Authentication.AuthenticationSchemes for the known external login providers.</returns>
        Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();

        /// <summary>
        /// Attempts to sign in the specified user and password combination as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user to sign in.</param>
        /// <param name="password">The password to attempt to sign in with.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
        /// <returns>The task object representing the asynchronous operation containing the SignInResult for the sign-in attempt.</returns>
        Task<SignInResult> PasswordSignInAsync(Models.Account.ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure);

        /// <summary>
        /// Signs in the specified user.
        /// </summary>
        /// <param name="user">The user to sign-in.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="authenticationMethod">Name of the method used to authenticate the user.</param>
        /// <returns>The task object representing the asynchronous operation.</returns>
        Task SignInAsync(Models.Account.ApplicationUser user, bool isPersistent, string authenticationMethod = null);

        /// <summary>
        /// Signs the current user out of the application.
        /// </summary>
        Task SignOutAsync();

        /// <summary>
        /// Configures the redirect URL and user identifier for the specified external login provider.
        /// </summary>
        /// <param name="provider">The provider to configure.</param>
        /// <param name="redirectUrl">The external login URL users should be redirected to during the login flow.</param>
        /// <param name="userId">The current user's identifier, which will be used to provide CSRF protection.</param>
        /// <returns>A configured Microsoft.AspNetCore.Authentication.AuthenticationProperties.</returns>
        Task<AuthenticationProperties> ConfigureExternalAuthenticationPropertiesAsync(string provider, string redirectUrl, string userId = null);

        /// <summary>
        /// Gets the external login information for the current login, as an asynchronous operation.
        /// </summary>
        /// <param name="expectedXsrf">Flag indication whether a Cross Site Request Forgery token was expected in the current request.</param>
        /// <returns>The task object representing the asynchronous operation containing the ExternalLoginInfo for the sign-in attempt.</returns>
        Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null);

        /// <summary>
        /// Signs in a user via a previously registered third party login, as an asynchronous operation.
        /// </summary>
        /// <param name="loginProvider">The login provider to use.</param>
        /// <param name="providerKey">The unique provider identifier for the user.</param>
        /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
        /// <param name="bypassTwoFactor">Flag indicating whether to bypass two factor authentication.</param>
        /// <returns>The task object representing the asynchronous operation containing the SignInResult for the sign-in attempt.</returns>
        Task<SignInResult> ExternalLoginSignInAsync(string loginProvider, string providerKey, bool isPersistent, bool bypassTwoFactor);
    }
}
