using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik
{
    public class MenuBreadcrumbAttribute : ActionFilterAttribute
    {
        public const string NAME = "MenuBreadcrumb";

        private readonly Type _type;
        public MenuBreadcrumbAttribute(Type type)
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
