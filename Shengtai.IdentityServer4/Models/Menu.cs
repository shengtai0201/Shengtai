using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer4.Models
{
    public class Menu : INavHeader, INavTreeView, INavItem
    {
        public bool Active { get; set; }

        public Paragraph Paragraph { get; set; }

        public string Url { get; set; }

        public string Icon { get; set; }

        public (INavHeader Header, INavTreeView TreeView) Parent { get; set; }

        public ICollection<INavTreeView> Menus { get; set; }

        public string Text { get; set; }
    }
}
