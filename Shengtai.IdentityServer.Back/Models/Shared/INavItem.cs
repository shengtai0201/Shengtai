using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Models.Shared
{
    public interface INavItem
    {
        bool Active { get; set; }

        // 不可為空
        Paragraph Paragraph { get; }

        // 為空("#")，Menus必有值；不為空，Menus必為空。
        string Url { get; }

        string Icon { get; }

        (INavHeader Header, INavTreeView TreeView) Parent { get; }
    }
}
