using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Models.Shared
{
    // 含樹狀結構的集合
    public interface INavTreeView : INavItem, IMenu
    {
        ICollection<INavTreeView> Menus { get; }

        //bool Show(dynamic role);
    }
}
