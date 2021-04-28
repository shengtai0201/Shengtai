using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public interface IUserService 
    {
        Task<Models.Account.ApplicationUser> FindByAccountAsync(string account);
        Task<IdentityResult> CreateAsync(Models.Account.ApplicationUser user, string password);
        Task<string> GenerateEmailConfirmationTokenAsync(Models.Account.ApplicationUser user);
        bool RequireConfirmedAccount { get; }
        Task<Models.Account.ApplicationUser> FindByIdAsync(string userId);
        Task<IdentityResult> ConfirmEmailAsync(Models.Account.ApplicationUser user, string token);
        Task<Models.Account.ApplicationUser> FindByEmailAsync(string email);
        Task<string> GetUserIdAsync(Models.Account.ApplicationUser user);
    }
}
