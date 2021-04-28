using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Models.Shared
{
    public interface INavTreeView : INavItem
    {
        ICollection<INavTreeView> Menus { get; }
    }
}
