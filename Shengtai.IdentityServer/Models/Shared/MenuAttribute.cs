using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Models.Shared
{
    public class MenuAttribute : ActionFilterAttribute
    {
        public const string NAME = "IMenu.Key";

        private int _key;

        public MenuAttribute(int key)
        {
            _key = key;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await base.OnActionExecutionAsync(context, next);

            if (context.Controller is Microsoft.AspNetCore.Mvc.Controller controller)
                controller.ViewData[NAME] = _key;

            await next();
        }
    }
}
