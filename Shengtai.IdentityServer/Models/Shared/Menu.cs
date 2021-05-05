using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Models.Shared
{
    public class Menu : INavHeader, INavTreeView, INavItem
    {
        public dynamic Key { get; set; }

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

                ShowEvent += _builder.ShowStrategy;
            }
        }

        public MenuTypes Type { get; set; }

        public event IMenu.ShowEventHandler ShowEvent;

        public bool Show(IList<string> roles)
        {
            bool result = false;
            if (this.Type != MenuTypes.Item)
                foreach (var menu in this.Menus)
                    result |= menu.Show(roles);

            result |= ShowEvent(this, roles);

            return result;
        }
    }
}
