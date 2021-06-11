using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public interface IIdentityServerService
    {
        Task<(string ClientId, string ClientSecret)> AddClientAsync(Models.Account.ApplicationUser user, IEnumerable<Claim> claims = null);
    }
}
