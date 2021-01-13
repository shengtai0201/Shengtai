using Microsoft.Extensions.Options;
using Shengtai.Options;
using System.Security.Principal;

namespace Shengtai.Data.Core
{
    public abstract class Repository<TDbContext, TAppSettings, TDefaultConnection> :
        Repository<TDbContext, TAppSettings, TDefaultConnection, IPrincipal>
        where TDbContext : class
        where TAppSettings : AppSettings<TDefaultConnection>, new()
        where TDefaultConnection : IDefaultConnection
    {
        protected Repository(IOptions<TAppSettings> options) : base(options?.Value)
        {
        }
    }
}