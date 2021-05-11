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
            var key = Convert.ToInt32(viewData[Models.Shared.MenuAttribute.NAME]);

            return builder.ReadBreadcrumbs(key);
        }

        public static bool IsActived(this ViewDataDictionary viewData, MenuBuilder builder, int targetKey)
        {
            var key = Convert.ToInt32(viewData[Models.Shared.MenuAttribute.NAME]);

            return builder.IsActived(key, targetKey);
        }
    }
}
