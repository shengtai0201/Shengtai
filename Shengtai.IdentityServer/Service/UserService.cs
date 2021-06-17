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

        public async Task<IdentityResult> AddClaimAsync(ApplicationUser user, string type, string value)
        {
            value = Cryptography.AES.Encrypt(value, type);

            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.AddClaimAsync(identityUser as TUser, new Claim(type, value));
        }

        public async Task<IdentityResult> AddClaimsAsync(ApplicationUser user, IDictionary<string, string> values)
        {
            var claims = values.Select(x => new Claim(x.Key, Cryptography.AES.Encrypt(x.Value, x.Key)));

            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.AddClaimsAsync(identityUser as TUser, claims);
        }

        public async Task<IdentityResult> AddLoginAsync(ApplicationUser user, UserLoginInfo login)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.AddLoginAsync(identityUser as TUser, login);
        }

        public async Task<IdentityResult> AddPasswordAsync(ApplicationUser user, string password)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.AddPasswordAsync(identityUser as TUser, password);
        }

        public async Task<IdentityResult> AddToRolesAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.AddToRolesAsync(identityUser as TUser, roles);
        }

        public async Task<IdentityResult> ChangePasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.ChangePasswordAsync(identityUser as TUser, currentPassword, newPassword);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(ApplicationUser user, string token)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.ConfirmEmailAsync(identityUser as TUser, token);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
        {
            var identityUser = _mapper.Map<TUser>(user);
            return await _userManager.CreateAsync(identityUser, password);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user)
        {
            var identityUser = _mapper.Map<TUser>(user);
            return await _userManager.CreateAsync(identityUser);
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

        public async Task<string> GenerateChangeEmailTokenAsync(ApplicationUser user, string newEmail)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.GenerateChangeEmailTokenAsync(identityUser as TUser, newEmail);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.GenerateEmailConfirmationTokenAsync(identityUser as TUser);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.GeneratePasswordResetTokenAsync(identityUser as TUser);
        }

        public async Task<string> GetClaimValueAsync(ClaimsPrincipal principal, string type)
        {
            var user = await _userManager.GetUserAsync(principal);
            var claims = await _userManager.GetClaimsAsync(user);
            var claim = claims.SingleOrDefault(x => x.Type == type);
            if (claim != null)
                return Cryptography.AES.Decrypt(claim.Value, type);

            return null;
        }

        public async Task<string> GetEmailAsync(ApplicationUser user)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.GetEmailAsync(identityUser as TUser);
        }

        public async Task<string> GetPhoneNumberAsync(ApplicationUser user)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.GetPhoneNumberAsync(identityUser as TUser);
        }

        public async Task<IList<string>> GetRolesAsync(ClaimsPrincipal principal)
        {
            var user = await _userManager.GetUserAsync(principal);
            IList<string> roles = null;
            if (user != null)
                roles = await _userManager.GetRolesAsync(user);

            return roles;
        }

        public async Task<ApplicationUser> GetUserAsync(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }

        public async Task<string> GetUserIdAsync(ApplicationUser user)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.GetUserIdAsync(identityUser as TUser);
        }

        public async Task<string> GetUserIdAsync(ClaimsPrincipal principal)
        {
            var identityUser = await _userManager.GetUserAsync(principal);
            return identityUser?.Id;
        }

        public async Task<string> GetUserNameAsync(ApplicationUser user)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.GetUserNameAsync(identityUser as TUser);
        }

        public async Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.HasPasswordAsync(identityUser as TUser);
        }

        public async Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.IsEmailConfirmedAsync(identityUser as TUser);
        }

        public Task<bool> RequireConfirmedAccountAsync()
        {
            var result = _userManager.Options.SignIn.RequireConfirmedAccount;
            return Task.FromResult(result);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.ResetPasswordAsync(identityUser as TUser, token, newPassword);
        }

        public async Task<IdentityResult> SetPhoneNumberAsync(ApplicationUser user, string phoneNumber)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.SetPhoneNumberAsync(identityUser as TUser, phoneNumber);
        }

        public async Task<IdentityResult> SetUserNameAsync(ApplicationUser user, string userName)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.SetUserNameAsync(identityUser as TUser, userName);
        }
    }
}
