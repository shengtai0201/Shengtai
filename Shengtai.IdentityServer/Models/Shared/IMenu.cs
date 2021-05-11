using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Models.Shared
{
    public interface IMenu
    {
        delegate bool ShowEventHandler(Menu sender, IList<string> roles);

        event ShowEventHandler ShowEvent;

        int Key { get; }

        Data.MenuTypes Type { get; }

        bool Show(IList<string> roles);
    }
}
