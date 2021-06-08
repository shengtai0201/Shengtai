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
    // todo: �{���e���|�ɦ� email �T�w page
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

            // todo: Ĳ�o�I�b client
            // todo: Register �e�n�[�J�� callback url
            // todo: �ɨ� client system's RegisterConfirmation

            return Page();
        }
    }
}
