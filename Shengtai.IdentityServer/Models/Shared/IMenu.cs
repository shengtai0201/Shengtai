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

        bool Show(IList<string> roles);

        Data.MenuTypes Type { get; }
    }
}
