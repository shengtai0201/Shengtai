using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Models.Shared
{
    public class Menu : INavHeader, INavTreeView, INavItem
    {
        //public Menu()
        //{
        //    this.ShowEvent += Builder.Show;
        //}

        public dynamic Key { get; set; }

        // 不設定權限，表示公開
        public dynamic Role { get; set; }

        public bool Active { get; set; }

        public Paragraph Paragraph { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        public (INavHeader Header, INavTreeView TreeView) Parent { get; set; }

        public ICollection<INavTreeView> Menus { get; set; }

        public string Text { get; set; }

        private MenuBuilder _builder;
        public MenuBuilder Builder
        {
            get
            {
                return _builder;
            }
            set
            {
                _builder = value;

                if (ShowEvent != null)
                    ShowEvent = null;

                ShowEvent += _builder.Show;
            }
        }

        public event IMenu.ShowEventHandler ShowEvent;

        public bool Show(IList<string> roles)
        {
            if (this.Role == null && (roles == null || roles.Count == 0))
                return true;

            bool result = false;
            if (this.Menus != null)
                foreach (var menu in this.Menus)
                    result |= menu.Show(roles);

            if (result && ShowEvent == null)
                return result;
            else if (ShowEvent != null)
                result |= ShowEvent(this, roles);
            else
                result = false;

            return result;
        }

        //public bool Show(dynamic role)
        //{
        //    if (!role.HasValue)
        //        return true;

        //    bool result = false;

        //    // 次目錄是否要顯示
        //    foreach (var menu in this.Menus)
        //        result |= menu.Show(role.Value);

        //    if (result && ShowEvent == null)
        //        return result;
        //    else if (ShowEvent != null)
        //        result |= ShowEvent(this, role.Value);
        //    else
        //        result = false;

        //    return result;
        //}
    }
}
