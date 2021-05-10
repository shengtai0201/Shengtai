using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Models.Shared
{
    // 可點選的連結
    public interface INavItem : IMenu
    {
        dynamic Key { get; set; }

        // 不設定權限，表示公開
        IList<string> Roles { get; set; }

        bool Active { get; set; }

        // 不可為空
        Paragraph Paragraph { get; }

        // 為空("#")，Menus必有值；不為空，Menus必為空。
        string Url { get; }

        string Icon { get; }

        (INavHeader Header, INavTreeView TreeView) Parent { get; }
    }
}
