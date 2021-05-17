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

        private readonly int _value;

        public MenuAttribute(string parentText, string parentSmall, string text, string small = null)
        {
            Paragraph parent = null;
            if (!string.IsNullOrEmpty(parentText))
                parent = new Paragraph { Text = parentText, Small = parentSmall };

            var key = new Paragraph { Text = text, Small = small, Parent = parent };

            _value = MenuBuilder.Menus[key];
        }

        public MenuAttribute(string text, string small = null) : this(null, null, text, small) { }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await base.OnActionExecutionAsync(context, next);

            if (context.Controller is Microsoft.AspNetCore.Mvc.Controller controller)
                controller.ViewData[NAME] = _value;

            await next();
        }
    }
}
