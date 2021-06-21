using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public interface IUserService
    {
        /// <summary>
        /// Finds and returns a user, if any, who has the specified user account.
        /// </summary>
        /// <param name="account">The user account to search for.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, containing the user matching the specified userId if it exists.</returns>
        Task<Models.Account.ApplicationUser> FindByAccountAsync(string account);

        /// <summary>
        /// Creates the specified user in the backing store with given password, as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <param name="password">The password for the user to hash and store.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, containing the Microsoft.AspNetCore.Identity.IdentityResult of the operation.</returns>
        Task<IdentityResult> CreateAsync(Models.Account.ApplicationUser user, string password);

        /// <summary>
        /// Creates the specified user in the backing store with no password, as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user to create.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, containing the Microsoft.AspNetCore.Identity.IdentityResult of the operation.</returns>
        Task<IdentityResult> CreateAsync(Models.Account.ApplicationUser user);

        /// <summary>
        /// Generates an email confirmation token for the specified user.
        /// </summary>
        /// <param name="user">The user to generate an email confirmation token for.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, an email confirmation token.</returns>
        Task<string> GenerateEmailConfirmationTokenAsync(Models.Account.ApplicationUser user);

        Task<bool> RequireConfirmedAccountAsync();

        /// <summary>
        /// Finds and returns a user, if any, who has the specified userId.
        /// </summary>
        /// <param name="userId">The user ID to search for.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, containing the user matching the specified userId if it exists.</returns>
        Task<Models.Account.ApplicationUser> FindByIdAsync(string userId);

        /// <summary>
        /// Validates that an email confirmation token matches the specified user.
        /// </summary>
        /// <param name="user">The user to validate the token against.</param>
        /// <param name="token">The email confirmation token to validate.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, containing the Microsoft.AspNetCore.Identity.IdentityResult of the operation.</returns>
        Task<IdentityResult> ConfirmEmailAsync(Models.Account.ApplicationUser user, string token);
        Task<Models.Account.ApplicationUser> FindByEmailAsync(string email);

        /// <summary>
        /// Gets the email address for the specified user.
        /// </summary>
        /// <param name="user">The user whose email should be returned.</param>
        /// <returns>The task object containing the results of the asynchronous operation, the email address for the specified user.</returns>
        Task<string> GetEmailAsync(Models.Account.ApplicationUser user);

        /// <summary>
        /// Generates an email change token for the specified user.
        /// </summary>
        /// <param name="user">The user to generate an email change token for.</param>
        /// <param name="newEmail">The new email address.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, an email change token.</returns>
        Task<string> GenerateChangeEmailTokenAsync(Models.Account.ApplicationUser user, string newEmail);

        /// <summary>
        /// Gets a flag indicating whether the specified user has a password.
        /// </summary>
        /// <param name="user">The user to return a flag for, indicating whether they have a password or not.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, returning true if the specified user has a password otherwise false.</returns>
        Task<bool> HasPasswordAsync(Models.Account.ApplicationUser user);

        Task<bool> HasAccountAsync(Models.Account.ApplicationUser user);

        /// <summary>
        /// Changes a user's password after confirming the specified currentPassword is correct, as an asynchronous operation.
        /// </summary>
        /// <param name="user">The user whose password should be set.</param>
        /// <param name="currentPassword">The current password to validate before changing.</param>
        /// <param name="newPassword">The new password to set for the specified user.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, containing the Microsoft.AspNetCore.Identity.IdentityResult of the operation.</returns>
        Task<IdentityResult> ChangePasswordAsync(Models.Account.ApplicationUser user, string currentPassword, string newPassword);

        /// <summary>
        /// Adds the password to the specified user only if the user does not already have a password.
        /// </summary>
        /// <param name="user">The user whose password should be set.</param>
        /// <param name="password">The password to set.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, containing the Microsoft.AspNetCore.Identity.IdentityResult of the operation.</returns>
        Task<IdentityResult> AddPasswordAsync(Models.Account.ApplicationUser user, string password);

        Task<IdentityResult> AddAccountAsync(Models.Account.ApplicationUser user, string account, string password);

        Task<string> GetUserIdAsync(Models.Account.ApplicationUser user);
        Task<string> GetUserIdAsync(ClaimsPrincipal principal);

        /// <summary>
        /// Returns the user corresponding to the IdentityOptions.ClaimsIdentity.UserIdClaimType claim in the principal or null.
        /// </summary>
        /// <param name="principal">The principal which contains the user id claim.</param>
        /// <returns>The user corresponding to the IdentityOptions.ClaimsIdentity.UserIdClaimType claim in the principal or null</returns>
        Task<Models.Account.ApplicationUser> GetUserAsync(ClaimsPrincipal principal);

        /// <summary>
        /// Gets the user name for the specified user.
        /// </summary>
        /// <param name="user">The user whose name should be retrieved.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, containing the name for the specified user.</returns>
        Task<string> GetUserNameAsync(Models.Account.ApplicationUser user);

        /// <summary>
        /// Retrieves the associated logins for the specified.
        /// </summary>
        /// <param name="user">The user whose associated logins to retrieve.</param>
        /// <returns>The System.Threading.Tasks.Task for the asynchronous operation, containing a list of Microsoft.AspNetCore.Identity.UserLoginInfo for the specified user, if any.</returns>
        Task<IList<UserLoginInfo>> GetLoginsAsync(Models.Account.ApplicationUser user);

        /// <summary>
        /// Sets the given userName for the specified user.
        /// </summary>
        /// <param name="user">The user whose name should be set.</param>
        /// <param name="userName">The user name to set.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation.</returns>
        Task<IdentityResult> SetUserNameAsync(Models.Account.ApplicationUser user, string userName);

        /// <summary>
        /// Gets the telephone number, if any, for the specified user.
        /// </summary>
        /// <param name="user">The user whose telephone number should be retrieved.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, containing the user's telephone number, if any.</returns>
        Task<string> GetPhoneNumberAsync(Models.Account.ApplicationUser user);

        /// <summary>
        /// Sets the phone number for the specified user.
        /// </summary>
        /// <param name="user">The user whose phone number to set.</param>
        /// <param name="phoneNumber">The phone number to set.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, containing the Microsoft.AspNetCore.Identity.IdentityResult of the operation.</returns>
        Task<IdentityResult> SetPhoneNumberAsync(Models.Account.ApplicationUser user, string phoneNumber);

        Task<IList<string>> GetRolesAsync(ClaimsPrincipal principal);
        Task<bool> IsEmailConfirmedAsync(Models.Account.ApplicationUser user);
        Task<string> GeneratePasswordResetTokenAsync(Models.Account.ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(Models.Account.ApplicationUser user, string token, string newPassword);
        Task<IdentityResult> AddToRolesAsync(Models.Account.ApplicationUser user, IEnumerable<string> roles);
        Task<IdentityResult> AddClaimAsync(Models.Account.ApplicationUser user, string type, string value);

        /// <summary>
        /// Adds the specified claims to the user.
        /// </summary>
        /// <param name="user">The user to add the claim to.</param>
        /// <param name="values">The claims to add.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, containing the Microsoft.AspNetCore.Identity.IdentityResult of the operation.</returns>
        Task<IdentityResult> AddClaimsAsync(Models.Account.ApplicationUser user, IDictionary<string, string> values);

        Task<string> GetClaimValueAsync(ClaimsPrincipal principal, string type);

        /// <summary>
        /// Adds an external Microsoft.AspNetCore.Identity.UserLoginInfo to the specified user.
        /// </summary>
        /// <param name="user">The user to add the login to.</param>
        /// <param name="login">The external Microsoft.AspNetCore.Identity.UserLoginInfo to add to the specified user.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, containing the Microsoft.AspNetCore.Identity.IdentityResult of the operation.</returns>
        Task<IdentityResult> AddLoginAsync(Models.Account.ApplicationUser user, UserLoginInfo login);

        /// <summary>
        /// Attempts to remove the provided external login information from the specified user. and returns a flag indicating whether the removal succeed or not.
        /// </summary>
        /// <param name="user">The user to remove the login information from.</param>
        /// <param name="loginProvider">The login provide whose information should be removed.</param>
        /// <param name="providerKey">The key given by the external login provider for the specified user.</param>
        /// <returns>The System.Threading.Tasks.Task that represents the asynchronous operation, containing the Microsoft.AspNetCore.Identity.IdentityResult of the operation.</returns>
        Task<IdentityResult> RemoveLoginAsync(Models.Account.ApplicationUser user, string loginProvider, string providerKey);
    }
}
