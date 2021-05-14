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
        private readonly IUserService _userService;
        private readonly IIdentityServerService _identityServerService;

        public RegisterConfirmationModel(IUserService userService, IIdentityServerService identityServerService)
        {
            _userService = userService;
            _identityServerService = identityServerService;
        }

        public string Email { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Scope { get; set; }

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

            // machine to machine client(api)
            var result = await _identityServerService.AddClientAsync(user);
            ClientId = result.ClientId;
            ClientSecret = result.ClientSecret;
            Scope = result.Scope;

            return Page();
        }
    }
}
