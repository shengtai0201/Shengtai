using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer4
{
    public interface IAppSettings
    {
        public class _IdentityServer4
        {
            public string ApplicationName { get; set; } = "AdminLTE";
            public string BrandLogo { get; set; } = "~/_content/Shengtai.IdentityServer4/lib/admin-lte/img/AdminLTELogo.png";
        }

        _IdentityServer4 IdentityServer4 { get; }
    }
}
