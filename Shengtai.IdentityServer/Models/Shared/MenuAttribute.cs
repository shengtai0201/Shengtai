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
        public const string NAME = "MENU_AUTH";

        private readonly Type _type;
        public MenuAttribute(Type type)
        {
            _type = type;
        }

        public object Key { get; set; }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await base.OnActionExecutionAsync(context, next);

            if (context.Controller is Microsoft.AspNetCore.Mvc.Controller controller && Key.GetType() == _type)
                controller.ViewData[NAME] = Convert.ChangeType(Key, _type);

            await next();
        }
    }
}
