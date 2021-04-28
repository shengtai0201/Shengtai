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
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly IUserService _userService;

        public RegisterConfirmationModel(IUserService userService)
        {
            _userService = userService;
        }

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
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
            // Once you add a real email sender, you should remove this code that lets you confirm the account
            DisplayConfirmAccountLink = true;
            if (DisplayConfirmAccountLink)
            {
                var userId = await _userService.GetUserIdAsync(user);
                var code = await _userService.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                EmailConfirmationUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null, values: new { area = "IdentityServer", userId, code, returnUrl }, protocol: Request.Scheme);
            }

            return Page();
        }
    }
}
