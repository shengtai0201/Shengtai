using Shengtai.Options;

namespace Shengtai.Data
{
    // 內含使用者資訊
    public abstract class Repository<TDbContext, TAppSettings, TDefaultConnection, TPrincipal> :
         Repository<TDbContext, TAppSettings, TDefaultConnection>, ICurrentUser<TPrincipal>
         where TDbContext : class
         where TAppSettings : AppSettings<TDefaultConnection>, new()
         where TDefaultConnection : IDefaultConnection
    {
        public TPrincipal CurrentUser { protected get; set; }

        protected Repository(TAppSettings appSettings = null) : base(appSettings)
        {
        }
    }
}