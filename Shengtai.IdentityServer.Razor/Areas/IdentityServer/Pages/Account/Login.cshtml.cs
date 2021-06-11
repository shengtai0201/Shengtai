using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using IdentityServer4.Services;
using Shengtai.IdentityServer.Models.Account;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Stores;

namespace Shengtai.IdentityServer.Areas.IdentityServer.Pages.Account
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly IAppSettings _appSettings;
        private readonly ISignInService _signInService;
        private readonly IUserService _userService;

        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IClientStore _clientStore;
        private readonly IEventService _eventService;

        public LoginModel(ILogger<LoginModel> logger, IAppSettings appSettings, ISignInService signInService, IUserService userService, 
            IIdentityServerInteractionService interactionService, IAuthenticationSchemeProvider schemeProvider, IClientStore clientStore, IEventService eventService)
        {
            _logger = logger;
            _appSettings = appSettings;
            _signInService = signInService;
            _userService = userService;
            _interactionService = interactionService;
            _schemeProvider = schemeProvider;
            _clientStore = clientStore;
            _eventService = eventService;
        }

        [BindProperty]
        public LoginViewModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        //public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        //public class InputModel
        //{
        //    [Required]
        //    public string Account { get; set; }

        //    [Required]
        //    [DataType(DataType.Password)]
        //    public string Password { get; set; }

        //    [Display(Name = "Remember me?")]
        //    public bool RememberMe { get; set; }
        //}

        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interactionService.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && await _schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Account = context?.LoginHint,
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes.Where(x => x.DisplayName != null).Select(x => new ExternalProvider { DisplayName = x.DisplayName ?? x.Name, AuthenticationScheme = x.Name }).ToList();

            var allowLocal = true;
            if (context?.Client.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            // ÀA¤W²Kªá
            foreach(var provider in providers)
            {
                switch (provider.AuthenticationScheme)
                {
                    case "Google":
                        provider.Icon = "btn-danger";
                        provider.Logo = "fa-google-plus";
                        provider.Text = "Sign in using Google+";
                        break;
                    case "Facebook":
                        provider.Icon = "btn-primary";
                        provider.Logo = "fa-facebook";
                        provider.Text = "Sign in using Facebook";
                        break;
                    case "Microsoft":
                        provider.Icon = "btn-warning";
                        provider.Logo = "fa-microsoft";
                        provider.Text = "Sign in using Microsoft account";
                        break;
                    case "Twitter":
                        provider.Icon = "btn-info";
                        provider.Logo = "fa-twitter";
                        provider.Text = "Sign in using Twitter";
                        break;
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = _appSettings.IdentityServer.Account.AllowRememberLogin,
                EnableLocalLogin = allowLocal && _appSettings.IdentityServer.Account.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Account = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
                ModelState.AddModelError(string.Empty, ErrorMessage);

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInService.GetExternalAuthenticationSchemesAsync()).ToList();

            //ReturnUrl = returnUrl;

            #region Identity Server 4
            // build a model so we know what to show on the login page
            Input = await BuildLoginViewModelAsync(returnUrl);

            if (Input.IsExternalLoginOnly)
                // we only have one option for logging in and it's an external provider
                RedirectToAction("Challenge", "External", new { scheme = Input.ExternalLoginScheme, returnUrl });
            #endregion
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Account = model.Account;
            vm.RememberMe = model.RememberMe;
            return vm;
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInService.GetExternalAuthenticationSchemesAsync()).ToList();

            #region Identity Server 4
            // check if we are in the context of an authorization request
            var context = await _interactionService.GetAuthorizationContextAsync(returnUrl);
            #endregion

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = Microsoft.AspNetCore.Identity.SignInResult.Failed;
                var user = await _userService.FindByAccountAsync(Input.Account);
                if(user != null)
                {
                    result = await _signInService.PasswordSignInAsync(user, Input.Password, Input.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User logged in.");

                        #region Identity Server 4
                        await _eventService.RaiseAsync(new UserLoginSuccessEvent(user.Account, user.Id, user.UserName, clientId: context?.Client.ClientId));

                        if (context != null)
                        {
                            if (context.IsNativeClient())
                                // The client is native, so this change in how to
                                // return the response is for better UX for the end user.
                                this.LoadingPage("Redirect", returnUrl);

                            // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                            return Redirect(returnUrl);
                        }

                        // request for a local page
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else if (string.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect("~/");
                        }
                        #endregion

                        return LocalRedirect(returnUrl);
                    }
                }

                #region Identity Server 4
                await _eventService.RaiseAsync(new UserLoginFailureEvent(Input.Account, "invalid credentials", clientId: context?.Client.ClientId));
                ModelState.AddModelError(string.Empty, _appSettings.IdentityServer.Account.InvalidCredentialsErrorMessage);
                #endregion

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
            }

            // something went wrong, show form with error
            Input = await this.BuildLoginViewModelAsync(Input);
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
