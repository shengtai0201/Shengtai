using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Models.Shared
{
    // 作為區分的標題
    public interface INavHeader : IMenu
    {
        // 可為空
        string Text { get; }

        // 不可為空
        ICollection<INavTreeView> Menus { get; }
    }
}
