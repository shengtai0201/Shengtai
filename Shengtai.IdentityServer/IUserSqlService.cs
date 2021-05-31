using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.IdentityServer
{
    public interface IUserSqlService
    {
        Task<string> GetUserIdAsync(string account);
    }
}
