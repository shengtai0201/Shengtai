using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer4
{
    public interface IIdentityServer4Service
    {
        bool IsSignedIn(ClaimsPrincipal principal);
    }
}
