using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Data
{
    public interface IDataStrategy
    {
        Task<IList<Models.Shared.Menu>> ReadAllAsync(Models.Shared.IMenu.ShowEventHandler showStrategy);

        void ReadBreadcrumbs(IList<Models.Shared.IMenu> menus, int key);
    }
}
