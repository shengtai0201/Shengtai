using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer.Models.Shared
{
    public interface IMenu
    {
        delegate bool ShowEventHandler(Menu sender, IList<string> args);

        event ShowEventHandler ShowEvent;

        MenuBuilder Builder { get; }

        bool Show(IList<string> roles);
    }
}
