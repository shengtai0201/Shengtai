using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public static class MenuExtensions
    {
        public static IList<Models.Shared.IMenu> ReadBreadcrumbs(this ViewDataDictionary viewData, MenuBuilder builder)
        {
            var key = viewData[Models.Shared.MenuAttribute.NAME].ToString();
            return builder.ReadBreadcrumbs(key);
        }
    }
}
