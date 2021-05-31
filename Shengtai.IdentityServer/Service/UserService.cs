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

        public async Task<IdentityResult> AddClaimAsync(ApplicationUser user, string type, string value)
        {
            value = Cryptography.AES.Encrypt(value, type);

            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.AddClaimAsync(identityUser as TUser, new Claim(type, value));
        }

        public async Task<IdentityResult> AddToRolesAsync(ApplicationUser user, IEnumerable<string> roles)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.AddToRolesAsync(identityUser as TUser, roles);
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

        //public async Task<string> GetClaimValueAsync(ApplicationUser user, string type)
        //{
        //    var identityUser = await this.ChangeTypeAsync<TUser>(user);
        //    var claims = await _userManager.GetClaimsAsync(identityUser as TUser);
        //    var claim = claims.SingleOrDefault(x => x.Type == type);
        //    if (claim != null)
        //        return Cryptography.AES.Decrypt(claim.Value, type);

        //    return null;
        //}

        public async Task<string> GetClaimValueAsync(ClaimsPrincipal principal, string type)
        {
            var user = await _userManager.GetUserAsync(principal);
            var claims = await _userManager.GetClaimsAsync(user);
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

        public async Task<string> GetUserIdAsync(ApplicationUser user)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.GetUserIdAsync(identityUser as TUser);
        }

        public async Task<bool> IsEmailConfirmedAsync(ApplicationUser user)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.IsEmailConfirmedAsync(identityUser as TUser);
        }

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string token, string newPassword)
        {
            var identityUser = await this.ChangeTypeAsync<TUser>(user);
            return await _userManager.ResetPasswordAsync(identityUser as TUser, token, newPassword);
        }
    }
}
