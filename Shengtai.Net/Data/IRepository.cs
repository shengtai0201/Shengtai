using Shengtai.Options;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Shengtai.Data
{
    public interface IRepository<out TAppSettings, out TDefaultConnection> : IClient
        where TAppSettings : AppSettings<TDefaultConnection>, new()
        where TDefaultConnection : IDefaultConnection
    {
        TAppSettings AppSettings { get; }

        Task DisposeAsync();

        DateTime? ToDateTime(object value);

        decimal? ToDecimal(object value);

        int? ToInt32(object value);

        string ToString(object value);

        Nullable<T> ToNullable<T>(bool isNull, T value) where T : struct;
    }
}