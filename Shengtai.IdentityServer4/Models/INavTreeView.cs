using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer4.Models
{
    public interface INavTreeView : INavItem
    {
        ICollection<INavTreeView> Menus { get; }
    }
}
