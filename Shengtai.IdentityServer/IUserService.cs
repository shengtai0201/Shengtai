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
        Task<string> GetUserIdAsync(Models.Account.ApplicationUser user);
        Task<string> GetUserIdAsync(ClaimsPrincipal principal);
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
    }
}
