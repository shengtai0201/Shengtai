using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Shengtai.IdentityServer.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public static class MenuExtensions
    {
        public static IList<IMenu> ReadBreadcrumbs(this ViewDataDictionary viewData, MenuBuilder builder)
        {
            var key = Convert.ToInt32(viewData[MenuAttribute.NAME]);

            return builder.ReadBreadcrumbs(key);
        }

        public static bool IsActived(this ViewDataDictionary viewData, MenuBuilder builder, int targetKey)
        {
            var key = Convert.ToInt32(viewData[MenuAttribute.NAME]);

            return builder.IsActived(key, targetKey);
        }

        public static INavHeader AddHeader(this IDictionary<int, Menu> database, int key, string text)
        {
            Menu header = new() { Key = key, Type = Data.MenuTypes.Header, Text = text };
            database.Add(key, header);

            return header;
        }

        public static INavTreeView AddTreeView(this INavHeader header, IDictionary<int, Menu> database, int key, string icon, string text, string small = null)
        {
            Menu treeView = new()
            {
                Key = key,
                Type = Data.MenuTypes.TreeView,
                Paragraph = new Paragraph { Text = text, Small = small, Parent = new Paragraph { Text = header.Text } },
                Icon = icon,
                Parent = (header, null)
            };

            database.Add(key, treeView);
            header.Menus.Add(treeView);

            return treeView;
        }

        public static INavTreeView AddItem(this INavTreeView treeView, IDictionary<int, Menu> database, int key, string icon, string text, string small, string url, params string[] roles)
        {
            Menu item = new()
            {
                Key = key,
                Type = Data.MenuTypes.Item,
                Roles = roles,
                Paragraph = new Paragraph { Text = text, Small = small, Parent = treeView.Paragraph },
                Icon = icon,
                Url = url,
                Parent = (null, treeView)
            };
            database.Add(key, item);
            treeView.Menus.Add(item);

            return treeView;
        }
    }
}
