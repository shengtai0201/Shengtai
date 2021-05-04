using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Views.Shared.Components
{
    [ViewComponent(Name = "Navigation")]
    public class NavigationViewComponent : ViewComponent
    {
        private readonly IUserService _userService;
        private readonly IList<Models.Shared.INavHeader> _headers;

        public NavigationViewComponent(IUserService userService, MenuBuilder builder)
        {
            _userService = userService;
            _headers = builder;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IList<string> roles = await _userService.GetRolesAsync(this.UserClaimsPrincipal);
            return View((_headers, roles));
        }
    }
}
