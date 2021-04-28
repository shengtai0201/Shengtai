using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Shengtai.IdentityServer.Models.Consent;

namespace Shengtai.IdentityServer.Areas.IdentityServer.Pages
{
    public class ConsentModel : PageModel
    {
        private readonly ILogger<ConsentModel> _logger;
        private readonly IAppSettings _appSettings;
        private readonly IIdentityServerInteractionService _interactionService;
        private readonly IEventService _eventService;

        public ConsentModel(ILogger<ConsentModel> logger, IAppSettings appSettings, IIdentityServerInteractionService interactionService,
            IEventService eventService)
        {
            _logger = logger;
            _appSettings = appSettings;
            _interactionService = interactionService;
            _eventService = eventService;
        }

        [BindProperty]
        public ConsentViewModel Input { get; set; }

        private async Task<ConsentViewModel> BuildViewModelAsync(string returnUrl, ConsentInputModel model = null)
        {
            var request = await _interactionService.GetAuthorizationContextAsync(returnUrl);
            if (request != null)
            {
                var vm = new ConsentViewModel
                {
                    RememberConsent = model?.RememberConsent ?? true,
                    ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>(),
                    Description = model?.Description,

                    ReturnUrl = returnUrl,

                    ClientName = request.Client.ClientName ?? request.Client.ClientId,
                    ClientUrl = request.Client.ClientUri,
                    ClientLogoUrl = request.Client.LogoUri,
                    AllowRememberConsent = request.Client.AllowRememberConsent
                };

                vm.IdentityScopes = request.ValidatedResources.Resources.IdentityResources.Select(identity => new ScopeViewModel
                {
                    Value = identity.Name,
                    DisplayName = identity.DisplayName ?? identity.Name,
                    Description = identity.Description,
                    Emphasize = identity.Emphasize,
                    Required = identity.Required,
                    Checked = (vm.ScopesConsented.Contains(identity.Name) || model == null) || identity.Required
                }).ToArray();

                var apiScopes = new List<ScopeViewModel>();
                foreach (var parsedScope in request.ValidatedResources.ParsedScopes)
                {
                    var apiScope = request.ValidatedResources.Resources.FindApiScope(parsedScope.ParsedName);
                    if (apiScope != null)
                    {
                        var displayName = apiScope.DisplayName ?? apiScope.Name;
                        if (!String.IsNullOrWhiteSpace(parsedScope.ParsedParameter))
                        {
                            displayName += ":" + parsedScope.ParsedParameter;
                        }

                        var scopeVm = new ScopeViewModel
                        {
                            Value = parsedScope.RawValue,
                            DisplayName = displayName,
                            Description = apiScope.Description,
                            Emphasize = apiScope.Emphasize,
                            Required = apiScope.Required,
                            Checked = (vm.ScopesConsented.Contains(parsedScope.RawValue) || model == null) || apiScope.Required
                        };

                        apiScopes.Add(scopeVm);
                    }
                }
                if (_appSettings.IdentityServer.Consent.EnableOfflineAccess && request.ValidatedResources.Resources.OfflineAccess)
                {
                    apiScopes.Add(new ScopeViewModel
                    {
                        Value = IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
                        DisplayName = _appSettings.IdentityServer.Consent.OfflineAccessDisplayName,
                        Description = _appSettings.IdentityServer.Consent.OfflineAccessDescription,
                        Emphasize = true,
                        Checked = vm.ScopesConsented.Contains(IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess) || model == null
                    });
                }
                vm.ApiScopes = apiScopes;

                return vm;
            }
            else
            {
                _logger.LogError("No consent request matching request: {0}", returnUrl);
            }

            return null;
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            Input = await BuildViewModelAsync(returnUrl);

            if (Input == null)
                RedirectToAction("Error");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostAsync(ConsentInputModel model)
        {
            var result = new ProcessConsentResult();

            // validate return url is still valid
            var request = await _interactionService.GetAuthorizationContextAsync(model.ReturnUrl);
            if (request != null)
            {
                ConsentResponse grantedConsent = null;

                // user clicked 'no' - send back the standard 'access_denied' response
                if (model?.Button == "no")
                {
                    grantedConsent = new ConsentResponse { Error = AuthorizationError.AccessDenied };

                    // emit event
                    await _eventService.RaiseAsync(new ConsentDeniedEvent(User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.RawScopeValues));
                }
                // user clicked 'yes' - validate the data
                else if (model?.Button == "yes")
                {
                    // if the user consented to some scope, build the response model
                    if (model.ScopesConsented != null && model.ScopesConsented.Any())
                    {
                        var scopes = model.ScopesConsented;
                        if (_appSettings.IdentityServer.Consent.EnableOfflineAccess == false)
                        {
                            scopes = scopes.Where(x => x != IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess);
                        }

                        grantedConsent = new ConsentResponse
                        {
                            RememberConsent = model.RememberConsent,
                            ScopesValuesConsented = scopes.ToArray(),
                            Description = model.Description
                        };

                        // emit event
                        await _eventService.RaiseAsync(new ConsentGrantedEvent(User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.RawScopeValues, grantedConsent.ScopesValuesConsented, grantedConsent.RememberConsent));
                    }
                    else
                    {
                        result.ValidationError = _appSettings.IdentityServer.Consent.MustChooseOneErrorMessage;
                    }
                }
                else
                {
                    result.ValidationError = _appSettings.IdentityServer.Consent.InvalidSelectionErrorMessage;
                }

                if (grantedConsent != null)
                {
                    // communicate outcome of consent back to identityserver
                    await _interactionService.GrantConsentAsync(request, grantedConsent);

                    // indicate that's it ok to redirect back to authorization endpoint
                    result.RedirectUri = model.ReturnUrl;
                    result.Client = request.Client;
                }
                else
                {
                    // we need to redisplay the consent UI
                    result.ViewModel = await BuildViewModelAsync(model.ReturnUrl, model);
                }
            }

            if (result.IsRedirect)
            {
                var context = await _interactionService.GetAuthorizationContextAsync(model.ReturnUrl);
                if (context?.IsNativeClient() == true)
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    this.LoadingPage("Redirect", result.RedirectUri);
                }

                return Redirect(result.RedirectUri);
            }

            if (result.HasValidationError)
            {
                ModelState.AddModelError(string.Empty, result.ValidationError);
            }

            if (result.ShowView)
            {
                return RedirectToAction("Index", result.ViewModel);
            }

            return RedirectToAction("Error");
        }
    }
}
