using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shengtai.IdentityServer.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Service
{
    public class UserService<TUser> : IUserService where TUser : ApplicationUser
    {
        private readonly UserManager<TUser> _userManager;

        public UserService(UserManager<TUser> userManager)
        {
            _userManager = userManager;
        }

        public bool RequireConfirmedAccount => _userManager.Options.SignIn.RequireConfirmedAccount;

        public Task<IdentityResult> AddToRolesAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            return _userManager.AddToRolesAsync(user as TUser, roles);
        }

        public Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            return _userManager.ConfirmEmailAsync(user as TUser, token);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return _userManager.CreateAsync(user as TUser, password);
        }

        public async Task<ApplicationUser> FindByAccountAsync(string account)
        {
            return await _userManager.Users.SingleOrDefaultAsync(u => u.Account == account);
        }

        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            return _userManager.GenerateEmailConfirmationTokenAsync(user as TUser);
        }

        public Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(user as TUser);
        }

        public async Task<IList<string>> GetRolesAsync(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            IList<string> roles = null;
            if (user != null)
                roles = await _userManager.GetRolesAsync(user);

            return roles;
        }

        public Task<string> GetUserIdAsync(ApplicationUser user)
        {
            return _userManager.GetUserIdAsync(user as TUser);
        }

        public Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            return _userManager.IsEmailConfirmedAsync(user as TUser);
        }

        public Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            return _userManager.ResetPasswordAsync(user as TUser, token, newPassword);
        }
    }
}
