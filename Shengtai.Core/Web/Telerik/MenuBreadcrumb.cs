using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik
{
    public class MenuBreadcrumb<TKey, TRole>
        where TKey : struct
        where TRole : struct
    {
        public delegate bool ShowEventHandler(MenuBreadcrumb<TKey, TRole> sender, TRole args);

        private MenuBreadcrumb()
        {
            Children = new List<MenuBreadcrumb<TKey, TRole>>();
        }

        public MenuBreadcrumb(TKey id, string name, TRole role, string icon, ShowEventHandler showEvent) : this()
        {
            this.Id = id;
            this.Name = name;
            this.Role = role;
            this.Icon = icon;
            this.ShowEvent += showEvent;
        }

        public MenuBreadcrumb(TKey id, string name, TRole role, string icon, ShowEventHandler showEvent, string url)
            : this(id, name, role, icon, showEvent)
        {
            this.Url = url;
        }

        public TKey Id { get; set; }

        public TRole Role { get; set; }

        public event ShowEventHandler ShowEvent;

        public bool Show(TRole role)
        {
            bool result = false;

            // 次目錄是否要顯示
            foreach (var child in this.Children)
                result |= child.Show(role);

            if (result && ShowEvent == null)
                return result;
            else if (ShowEvent != null)
                result |= ShowEvent(this, role);
            else
                result = false;

            return result;
        }

        [JsonIgnore]
        public MenuBreadcrumb<TKey, TRole> Parent { get; private set; } = null;

        [JsonIgnore]
        public IList<MenuBreadcrumb<TKey, TRole>> Children { get; private set; }

        /// <summary>
        /// 作為次層目錄設定上層關聯
        /// </summary>
        /// <param name="parent">父節點</param>
        /// <returns>本節點</returns>
        public MenuBreadcrumb<TKey, TRole> SetParent(MenuBreadcrumb<TKey, TRole> parent)
        {
            parent.HasChildren = true;
            parent.Children.Add(this);

            this.Parent = parent;
            this.Type = "item";

            return this;
        }

        /// <summary>
        /// 作為主層目錄設定上層關聯
        /// </summary>
        /// <param name="parent">父節點</param>
        /// <returns>本節點</returns>
        public MenuBreadcrumb<TKey, TRole> SetParentByRoot(MenuBreadcrumb<TKey, TRole> parent)
        {
            parent.HasChildren = true;
            parent.Children.Add(this);

            this.Parent = parent;
            this.Type = "rootitem";

            return this;
        }

        public bool HasChildren { get; private set; } = false;

        [JsonProperty("type")]
        public string Type { get; private set; } = "rootitem";

        [JsonProperty("href")]
        public string Url { get; set; }

        private string _name;
        [JsonProperty("text")]
        public string Name
        {
            get { return _name; }
            set
            {
                this.ShowText = !string.IsNullOrEmpty(value);
                _name = value;
            }
        }

        [JsonProperty("showText")]
        public bool ShowText { get; private set; } = false;

        private string _icon;
        [JsonProperty("icon")]
        public string Icon
        {
            get { return _icon; }
            set
            {
                this.ShowIcon = !string.IsNullOrEmpty(value);
                _icon = value;
            }
        }

        [JsonProperty("showIcon")]
        public bool ShowIcon { get; private set; } = false;

        public static void SetBreadcrumbItems(MenuBreadcrumb<TKey, TRole> menuBreadcrumb, IList<MenuBreadcrumb<TKey, TRole>> value)
        {
            if (menuBreadcrumb.Parent != null)
                SetBreadcrumbItems(menuBreadcrumb.Parent, value);

            value.Add(menuBreadcrumb);
        }

        public string ToBreadcrumbItemsJson()
        {
            var value = new List<MenuBreadcrumb<TKey, TRole>>();
            SetBreadcrumbItems(this, value);

            return JsonConvert.SerializeObject(value, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
    }
}
