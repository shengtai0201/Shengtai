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
        private readonly AutoMapper.IMapper _mapper;
        private readonly UserManager<TUser> _userManager;

        public UserService(AutoMapper.IMapper mapper, UserManager<TUser> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public bool RequireConfirmedAccount => _userManager.Options.SignIn.RequireConfirmedAccount;

        public Task<IdentityResult> AddClaimAsync(ApplicationUser user, string type, string value)
        {
            value = Cryptography.AES.Encrypt(value, type);

            return _userManager.AddClaimAsync(_mapper.ChangeType<ApplicationUser, TUser>(user), new Claim(type, value));
        }

        public Task<IdentityResult> AddToRolesAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            return _userManager.AddToRolesAsync(_mapper.ChangeType<ApplicationUser, TUser>(user), roles);
        }

        public Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            return _userManager.ConfirmEmailAsync(_mapper.ChangeType<ApplicationUser, TUser>(user), token);
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            return _userManager.CreateAsync(_mapper.ChangeType<ApplicationUser, TUser>(user), password);
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
            return _userManager.GenerateEmailConfirmationTokenAsync(_mapper.ChangeType<ApplicationUser, TUser>(user));
        }

        public Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return _userManager.GeneratePasswordResetTokenAsync(_mapper.ChangeType<ApplicationUser, TUser>(user));
        }

        public async Task<string> GetClaimValueAsync(ApplicationUser user, string type)
        {
            var claims = await _userManager.GetClaimsAsync(_mapper.ChangeType<ApplicationUser, TUser>(user));
            var claim = claims.SingleOrDefault(x => x.Type == type);
            if (claim != null)
                return Cryptography.AES.Decrypt(claim.Value, type);

            return null;
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
            return _userManager.GetUserIdAsync(_mapper.ChangeType<ApplicationUser, TUser>(user));
        }

        public async Task<string> GetUserIdAsync(string account)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Account == account);
            if (user != null)
                return user.Id;

            return null;
        }

        public Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            return _userManager.IsEmailConfirmedAsync(_mapper.ChangeType<ApplicationUser, TUser>(user));
        }

        public Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            return _userManager.ResetPasswordAsync(_mapper.ChangeType<ApplicationUser, TUser>(user), token, newPassword);
        }
    }
}
