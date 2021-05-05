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
        private readonly MenuBuilder _builder;

        public NavigationViewComponent(IUserService userService, MenuBuilder builder)
        {
            _userService = userService;
            _builder = builder;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var headers = _builder.GetHeaders();
            var roles = await _userService.GetRolesAsync(this.UserClaimsPrincipal);

            return View((headers, roles));
        }
    }
}
