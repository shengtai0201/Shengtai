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
        Task<Models.Account.ApplicationUser> FindByAccountAsync(string account);
        Task<IdentityResult> CreateAsync(Models.Account.ApplicationUser user, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(Models.Account.ApplicationUser user);
        Task<bool> RequireConfirmedAccountAsync();
        Task<Models.Account.ApplicationUser> FindByIdAsync(string userId);
        Task<IdentityResult> ConfirmEmailAsync(Models.Account.ApplicationUser user, string token);
        Task<Models.Account.ApplicationUser> FindByEmailAsync(string email);
        Task<string> GetUserIdAsync(Models.Account.ApplicationUser user);
        Task<IList<string>> GetRolesAsync(ClaimsPrincipal principal);
        Task<bool> IsEmailConfirmedAsync(Models.Account.ApplicationUser user);
        Task<string> GeneratePasswordResetTokenAsync(Models.Account.ApplicationUser user);
        Task<IdentityResult> ResetPasswordAsync(Models.Account.ApplicationUser user, string token, string newPassword);
        Task<IdentityResult> AddToRolesAsync(Models.Account.ApplicationUser user, IEnumerable<string> roles);
        Task<IdentityResult> AddClaimAsync(Models.Account.ApplicationUser user, string type, string value);
        Task<string> GetClaimValueAsync(ClaimsPrincipal principal, string type);
    }
}
