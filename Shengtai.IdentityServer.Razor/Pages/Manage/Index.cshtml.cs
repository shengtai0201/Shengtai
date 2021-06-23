using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Shengtai.IdentityServer.Pages.Manage
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IUserService _userService;
        private readonly ISignInService _signInService;
        private readonly IEmailService _emailService;

        public IndexModel(ILogger<IndexModel> logger, IUserService userService, ISignInService signInService, IEmailService emailService)
        {
            _logger = logger;
            _userService = userService;
            _signInService = signInService;
            _emailService = emailService;
        }

        public class ProfileModel
        {
            [StringLength(256)]
            [Display(Name = "User name")]
            public string UserName { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public class EmailModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "New email")]
            public string NewEmail { get; set; }
        }

        public bool HasAccount { get; set; }
        public class ChangePasswordModel
        {
            [StringLength(256)]
            [Display(Name = "User account")]
            public string Account { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationScheme> OtherLogins { get; set; }
        public bool ShowRemoveButton { get; set; }

        public class InputModel
        {
            public ProfileModel Profile { get; set; }
            public EmailModel Email { get; set; }
            public ChangePasswordModel ChangePassword { get; set; }
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnGetAsync(string pageHandler = null)
        {
            var user = await _userService.GetUserAsync(User);
            if (user == null)
                return NotFound($"Unable to load user with ID '{await _userService.GetUserIdAsync(User)}'.");

            if(pageHandler == "LinkLoginCallback")
            {
                var info = await _signInService.GetExternalLoginInfoAsync(user.Id);
                if (info == null)
                    throw new InvalidOperationException($"Unexpected error occurred loading external login info for user with ID '{user.Id}'.");

                var result = await _userService.AddLoginAsync(user, info);
                if (!result.Succeeded)
                {
                    StatusMessage = "The external login was not added. External logins can only be associated with one account.";
                    return RedirectToPage();
                }

                // Clear the existing external cookie to ensure a clean login process
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

                StatusMessage = "The external login was added.";
                return RedirectToPage();
            }
            else
            {
                Input = new InputModel();
                await LoadProfileAsync(user);
                await LoadEmailAsync(user);
                await LoadChangePasswordAsync(user);
                await LoadExternalLoginsAsync(user);

                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(string pageHandler, string loginProvider = null, string providerKey = null, string provider = null)
        {
            var user = await _userService.GetUserAsync(User);
            if (user == null)
                return NotFound($"Unable to load user with ID '{await _userService.GetUserIdAsync(User)}'.");

            return pageHandler switch
            {
                "Profile" => await PostProfileAsync(user),
                "ChangeEmail" => await ChangeEmailAsync(user),
                "SendVerificationEmail" => await SendVerificationEmailAsync(user),
                "CreateAccount" => await CreateAccountAsync(user),
                "ChangePassword" => await ChangePasswordAsync(user),
                "RemoveLogin" => await RemoveLoginAsync(user, loginProvider, providerKey),
                "LinkLogin" => await LinkLoginAsync(provider),
                _ => throw new ArgumentException(),
            };
        }

        private async Task LoadProfileAsync(Models.Account.ApplicationUser user)
        {
            var userName = await _userService.GetUserNameAsync(user);
            var phoneNumber = await _userService.GetPhoneNumberAsync(user);

            Input.Profile = new ProfileModel
            {
                UserName = userName,
                PhoneNumber = phoneNumber
            };
        }

        private async Task LoadEmailAsync(Models.Account.ApplicationUser user)
        {
            Email = await _userService.GetEmailAsync(user);

            Input.Email = new EmailModel
            {
                NewEmail = Email
            };

            IsEmailConfirmed = await _userService.IsEmailConfirmedAsync(user);
        }

        private async Task LoadChangePasswordAsync(Models.Account.ApplicationUser user)
        {
            Input.ChangePassword = new ChangePasswordModel
            {
                Account = user.Account
            };

            HasAccount = await _userService.HasAccountAsync(user);
        }

        private async Task LoadExternalLoginsAsync(Models.Account.ApplicationUser user)
        {
            CurrentLogins = await _userService.GetLoginsAsync(user);
            OtherLogins = (await _signInService.GetExternalAuthenticationSchemesAsync())
                .Where(auth => CurrentLogins.All(ul => auth.Name != ul.LoginProvider))
                .ToList();
            ShowRemoveButton = user.PasswordHash != null || CurrentLogins.Count > 1;
        }

        private async Task<IActionResult> PostProfileAsync(Models.Account.ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                await LoadProfileAsync(user);
                return Page();
            }

            var userName = await _userService.GetUserNameAsync(user);
            if(Input.Profile.UserName != userName)
            {
                var result = await _userService.SetUserNameAsync(user, Input.Profile.UserName);
                if (!result.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set user name.";
                    return RedirectToPage();
                }
            }

            var phoneNumber = await _userService.GetPhoneNumberAsync(user);
            if(Input.Profile.PhoneNumber != phoneNumber)
            {
                var result = await _userService.SetPhoneNumberAsync(user, Input.Profile.PhoneNumber);
                if (!result.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInService.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        private async Task<IActionResult> ChangeEmailAsync(Models.Account.ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                await LoadEmailAsync(user);
                return Page();
            }

            var email = await _userService.GetEmailAsync(user);
            if(Input.Email.NewEmail != email)
            {
                var userId = await _userService.GetUserIdAsync(user);
                var code = await _userService.GenerateChangeEmailTokenAsync(user, Input.Email.NewEmail);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page("/Account/ConfirmEmailChange", pageHandler: null, values: new { area = "IdentityServer", userId, email = Input.Email.NewEmail, code }, protocol: Request.Scheme);

                await _emailService.SendEmailAsync(Input.Email.NewEmail, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                StatusMessage = "Confirmation link to change email sent. Please check your email.";
                return RedirectToPage();
            }

            StatusMessage = "Your email is unchanged.";
            return RedirectToPage();
        }

        private async Task<IActionResult> SendVerificationEmailAsync(Models.Account.ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                await LoadEmailAsync(user);
                return Page();
            }

            var userId = await _userService.GetUserIdAsync(user);
            var email = await _userService.GetEmailAsync(user);
            var code = await _userService.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null, values: new { area = "IdentityServer", userId, code }, protocol: Request.Scheme);

            await _emailService.SendEmailAsync(email, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }

        private async Task<IActionResult> CreateAccountAsync(Models.Account.ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                await LoadChangePasswordAsync(user);
                return Page();
            }

            var result = await _userService.AddAccountAsync(user, Input.ChangePassword.Account, Input.ChangePassword.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return Page();
            }

            await _signInService.RefreshSignInAsync(user);
            StatusMessage = "Your account has been created.";

            return RedirectToPage();
        }

        private async Task<IActionResult> ChangePasswordAsync(Models.Account.ApplicationUser user)
        {
            if (!ModelState.IsValid)
            {
                await LoadChangePasswordAsync(user);
                return Page();
            }

            var result = await _userService.ChangePasswordAsync(user, Input.ChangePassword.OldPassword, Input.ChangePassword.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return Page();
            }

            await _signInService.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToPage();
        }

        private async Task<IActionResult> RemoveLoginAsync(Models.Account.ApplicationUser user, string loginProvider, string providerKey)
        {
            var result = await _userService.RemoveLoginAsync(user, loginProvider, providerKey);
            if (!result.Succeeded)
            {
                StatusMessage = "The external login was not removed.";
                return RedirectToPage();
            }

            await _signInService.RefreshSignInAsync(user);
            StatusMessage = "The external login was removed.";
            return RedirectToPage();
        }

        private async Task<IActionResult> LinkLoginAsync(string provider)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            // Request a redirect to the external login provider to link a login for the current user
            var redirectUrl = "Manage/Index?pageHandler=LinkLoginCallback";
            var userId = await _userService.GetUserIdAsync(User);
            var properties = await _signInService.ConfigureExternalAuthenticationPropertiesAsync(provider, redirectUrl, userId);
            return new ChallengeResult(provider, properties);
        }
    }
}
