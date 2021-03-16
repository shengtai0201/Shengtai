using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik
{
    public class MenuBreadcrumb<TRole> where TRole: struct
    {
        private MenuBreadcrumb()
        {
            Menus = new List<MenuBreadcrumb<TRole>> { this };
        }

        public MenuBreadcrumb(int id, string name, TRole role) : this()
        {
            this.Id = id;
            this.Name = name;
            this.Role = role;
        }

        public MenuBreadcrumb(int id, string name, TRole role, string url) : this(id, name, role)
        {
            this.Url = url;
        }

        public int Id { get; set; }

        public TRole Role { get; set; }

        public IList<MenuBreadcrumb<TRole>> Menus { get; private set; }

        [JsonIgnore]
        public MenuBreadcrumb<TRole> Parent { get; private set; } = null;

        public MenuBreadcrumb<TRole> SetParent(MenuBreadcrumb<TRole> parent)
        {
            parent.HasChildren = true;
            this.Parent = parent;

            Menus.Clear();
            Menus = new List<MenuBreadcrumb<TRole>>(this.Parent.Menus)
            {
                //this.Parent,
                this
            };

            return this;
        }

        public bool HasChildren { get; private set; } = false;

        [JsonProperty("type")]
        public string Type { get; private set; } = "rootitem";

        public MenuBreadcrumb<TRole> SetType(bool isRoot = true)
        {
            this.Type = isRoot ? "rootitem" : "item";

            return this;
        }

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

        public string ToBreadcrumbItemsJson()
        {
            return JsonConvert.SerializeObject(this.Menus, Formatting.None, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }
    }
}
