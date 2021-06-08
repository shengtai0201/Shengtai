using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace Shengtai.IdentityServer.Areas.IdentityServer.Pages.Account
{
    // todo: 現有畫面會導至 email 確定 page
    [SecurityHeaders]
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly IAppSettings _appSettings;
        private readonly IUserService _userService;

        public RegisterConfirmationModel(IAppSettings appSettings, IUserService userService)
        {
            _appSettings = appSettings;
            _userService = userService;
        }

        public string Email { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string clientId, string clientSecret, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await _userService.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            ClientId = clientId;
            ClientSecret = clientSecret;
            Scope = _appSettings.IdentityServer.Configuration.ApiScopeName;

            // todo: 觸發點在 client
            // todo: Register 前要加入該 callback url
            // todo: 導到 client system's RegisterConfirmation

            return Page();
        }
    }
}
