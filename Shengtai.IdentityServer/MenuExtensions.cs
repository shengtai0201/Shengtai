using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public static class MenuExtensions
    {
        // 回傳所新增之物件
        public static Models.Shared.INavHeader AddHeader(this MenuBuilder builder)
        {
            var header = new Models.Shared.Menu { Builder = builder, Menus = new List<Models.Shared.INavTreeView>() };
            builder.Add(header);

            return header;
        }

        // 回傳所新增之物件
        public static Models.Shared.INavHeader AddHeader(this MenuBuilder builder, string text)
        {
            var header = new Models.Shared.Menu { Builder = builder, Menus = new List<Models.Shared.INavTreeView>(), Text = text };
            builder.Add(header);

            return header;
        }

        // 回傳所新增之物件
        public static Models.Shared.INavTreeView AddTreeView(this Models.Shared.INavHeader header, string text, string icon)
        {
            var tv = new Models.Shared.Menu { Builder = header.Builder, Paragraph = new Models.Shared.Paragraph { Text = text }, Icon = icon, Menus = new List<Models.Shared.INavTreeView>(), Parent = (header, null) };
            header.Menus.Add(tv);

            return tv;
        }

        // 回傳所新增之物件
        public static Models.Shared.INavTreeView AddTreeView(this Models.Shared.INavHeader header, string text, string icon, string badgeClass, string badgeText)
        {
            var tv = new Models.Shared.Menu { Builder = header.Builder, Paragraph = new Models.Shared.Paragraph { Text = text, Badge = new Models.Shared.Badge { Class = badgeClass, Text = badgeText } }, Icon = icon, Menus = new List<Models.Shared.INavTreeView>(), Parent = (header, null) };
            header.Menus.Add(tv);

            return tv;
        }

        // 回傳所新增之物件
        public static Models.Shared.INavTreeView AddTreeView(this Models.Shared.INavTreeView treeView, string text, string icon)
        {
            var tv = new Models.Shared.Menu { Builder = treeView.Builder, Paragraph = new Models.Shared.Paragraph { Text = text }, Menus = new List<Models.Shared.INavTreeView>(), Parent = (null, treeView), Icon = icon };
            treeView.Menus.Add(tv);

            return tv;
        }

        private static void SetActive(Models.Shared.INavTreeView treeView)
        {
            if (treeView.Parent.TreeView != null)
                SetActive(treeView.Parent.TreeView);

            treeView.Active = true;
        }

        // 回傳父物件
        public static Models.Shared.INavTreeView AddItem(this Models.Shared.INavTreeView treeView, dynamic key, dynamic role, string text, string icon, string url, bool active = false)
        {
            var item = new Models.Shared.Menu
            {
                Builder = treeView.Builder,
                Key = key,
                Role = role,
                Paragraph = new Models.Shared.Paragraph { Text = text },
                Url = url,
                Active = active,
                Parent = (null, treeView),
                Icon = icon
            };
            treeView.Menus.Add(item);

            if (active)
                SetActive(treeView);

            return treeView;
        }

        // 回傳父物件
        public static Models.Shared.INavHeader AddItem(this Models.Shared.INavHeader header, dynamic key, dynamic role, string text, string icon, string url, string badgeClass, string badgeText, bool active = false)
        {
            var item = new Models.Shared.Menu
            {
                Builder = header.Builder,
                Key = key,
                Role = role,
                Paragraph = new Models.Shared.Paragraph { Text = text, Badge = new Models.Shared.Badge { Class = badgeClass, Text = badgeText } },
                Url = url,
                Icon = icon,
                Active = active,
                Parent = (header, null)
            };
            header.Menus.Add(item);

            return header;
        }

        // 回傳父物件
        public static Models.Shared.INavTreeView AddItem(this Models.Shared.INavTreeView treeView, dynamic key, dynamic role, string text, string icon, string url, string small, bool active = false)
        {
            var item = new Models.Shared.Menu
            {
                Builder = treeView.Builder,
                Key = key,
                Role = role,
                Paragraph = new Models.Shared.Paragraph { Text = text, Small = small },
                Url = url,
                Active = active,
                Parent = (null, treeView),
                Icon = icon
            };
            treeView.Menus.Add(item);

            if (active)
                SetActive(treeView);

            return treeView;
        }

        // 回傳父物件
        public static Models.Shared.INavHeader AddItem(this Models.Shared.INavHeader header, dynamic key, dynamic role, string text, string icon, string url, bool active = false)
        {
            var item = new Models.Shared.Menu
            {
                Builder = header.Builder,
                Key = key,
                Role = role,
                Paragraph = new Models.Shared.Paragraph { Text = text },
                Url = url,
                Icon = icon,
                Active = active,
                Parent = (header, null)
            };
            header.Menus.Add(item);

            return header;
        }
    }
}
