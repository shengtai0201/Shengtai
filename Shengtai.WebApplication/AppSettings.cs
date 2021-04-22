using Shengtai.IdentityServer4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shengtai.WebApplication
{
    public class AppSettings : Options.AppSettings<AppSettings, AppSettings._ConnectionStrings>, IAppSettings
    {
        public IAppSettings._IdentityServer4 IdentityServer4 { get; set; }

        public class _ConnectionStrings : Options.IConnectionStrings
        {
            public string DefaultConnection { get; set; }
        }
    }
}
