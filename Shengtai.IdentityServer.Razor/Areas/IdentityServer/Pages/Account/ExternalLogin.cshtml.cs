using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Shengtai.IdentityServer.Areas.IdentityServer.Pages.Account
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly ISignInService _signInService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IIdentityServerService _identityServerService;

        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IEventService _eventService;

        public ExternalLoginModel(ILogger<ExternalLoginModel> logger, ISignInService signInService, IUserService userService,
            IEmailService emailService, IIdentityServerService identityServerService, IIdentityServerInteractionService interactionService, 
            IEventService eventService)
        {
            _logger = logger;
            _signInService = signInService;
            _userService = userService;
            _emailService = emailService;
            _identityServerService = identityServerService;

            _interactionService = interactionService;
            _eventService = eventService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "姓名")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "電子郵件")]
            public string Email { get; set; }
        }

        public IActionResult OnGetAsync()
        {
            return RedirectToPage("./Login");
        }

        public async Task<IActionResult> OnPostAsync(string provider, string returnUrl = null)
        {
            if (string.IsNullOrEmpty(returnUrl))
                returnUrl = "~/";

            // validate returnUrl - either it is a valid OIDC URL or back to a local page
            if(!Url.IsLocalUrl(returnUrl) && !_interactionService.IsValidReturnUrl(returnUrl))
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }

            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = await _signInService.ConfigureExternalAuthenticationPropertiesAsync(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl ??= Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            var info = await _signInService.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
            
            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInService.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                }
                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            // read external identity from the temporary cookie
            var authenticateResult = await HttpContext.AuthenticateAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);
            if (authenticateResult?.Succeeded != true)
                _logger.LogError("External authentication error");

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                var externalClaims = authenticateResult.Principal.Claims.Select(c => $"{c.Type}: {c.Value}");
                _logger.LogDebug("External claims: {@claims}", externalClaims);
            }

            returnUrl ??= Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInService.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            if (ModelState.IsValid)
            {
                var user = new Models.Account.ApplicationUser { UserName = Input.UserName, Email = Input.Email };

                var result = await _userService.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userService.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

                        var userId = await _userService.GetUserIdAsync(user);
                        var code = await _userService.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page("/Account/ConfirmEmail", pageHandler: null, values: new { area = "IdentityServer", userId = userId, code = code }, protocol: Request.Scheme);

                        await _emailService.SendEmailAsync(Input.Email, "Confirm your email", $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        // machine to machine client(api)
                        var (ClientId, ClientSecret) = await _identityServerService.AddClientAsync(user, info.Principal.Claims);

                        // If account confirmation is required, we need to show the link if we don't have a real email sender
                        if (await _userService.RequireConfirmedAccountAsync())
                            return RedirectToPage("./RegisterConfirmation", new { email = Input.Email, clientId = ClientId, clientSecret = ClientSecret, returnUrl });
                        else
                        {
                            await _signInService.SignInAsync(user, isPersistent: false, info.LoginProvider);

                            // retrieve return URL
                            returnUrl = authenticateResult.Properties?.Items["returnUrl"] ?? "~/";

                            // check if external login is in the context of an OIDC request
                            var context = await _interactionService.GetAuthorizationContextAsync(returnUrl);
                            var providerUserId = info.Principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                            await _eventService.RaiseAsync(new IdentityServer4.Events.UserLoginSuccessEvent(authenticateResult.Properties?.Items["scheme"], providerUserId, user.Id, user.UserName, true, context?.Client.ClientId));

                            if(context != null)
                            {
                                if (context.IsNativeClient())
                                {
                                    // The client is native, so this change in how to
                                    // return the response is for better UX for the end user.
                                    this.LoadingPage("Redirect", returnUrl);
                                }
                            }

                            return LocalRedirect(returnUrl);
                        }
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
            return Page();
        }
    }
}
