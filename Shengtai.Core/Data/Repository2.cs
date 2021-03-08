using Shengtai.Options;
using System;
using System.Threading.Tasks;

namespace Shengtai.Data
{
    // used for windows, 不含使用者資訊
    public abstract class Repository<TDbContext, TAppSettings, TDefaultConnection> : Repository<TDbContext>,
         IRepository<TAppSettings, TDefaultConnection>
         where TDbContext : class
         where TAppSettings : AppSettings<TDefaultConnection>, new()
         where TDefaultConnection : IConnectionStrings
    {
        public TAppSettings AppSettings { get; private set; }

        protected Repository(TAppSettings appSettings = null) : base()
        {
            if (appSettings == null && CommonServiceLocator.ServiceLocator.IsLocationProviderSet)
                this.AppSettings = CommonServiceLocator.ServiceLocator.Current.GetInstance<TAppSettings>();
            else
                this.AppSettings = appSettings;
        }

        public virtual Task DisposeAsync()
        {
            return Task.FromResult<object>(null);
        }

        public DateTime? ToDateTime(object value)
        {
            if (value == null)
                return null;
            else
                return Convert.ToDateTime(value);
        }

        public decimal? ToDecimal(object value)
        {
            if (value == null)
                return null;
            else
                return Convert.ToDecimal(value);
        }

        public int? ToInt32(object value)
        {
            if (value == null)
                return null;
            else
                return Convert.ToInt32(value);
        }

        public string ToString(object value)
        {
            if (value == null)
                return null;
            else
                return value.ToString();
        }

        public T? ToNullable<T>(bool isNull, T value) where T : struct
        {
            if (isNull)
                return null;
            else
                return value;
        }
    }
}