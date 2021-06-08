using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public interface IRoleService
    {
        Task<Microsoft.AspNetCore.Identity.IdentityRole> FindByIdAsync(string roleId);
    }
}
