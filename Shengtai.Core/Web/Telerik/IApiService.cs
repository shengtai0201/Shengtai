using Shengtai.Data;
using Shengtai.Options;
using System;
using System.Threading.Tasks;

namespace Shengtai.Web.Telerik
{
    public interface IApiService<in TKey, TModel, in TPrincipal> : ICurrentUser<TPrincipal>
        where TModel : class
    {
        Task<bool> CreateAsync(TModel model, IDataSource dataSource);

        Task<IDataSourceResponse<TModel>> ReadAsync(IDataSourceRequest request);

        Task<bool?> UpdateAsync(TKey key, TModel model, IDataSource dataSource);

        Task<bool?> DestroyAsync(TKey key);

        Task DisposeAsync();
    }

    //  無法有直接對應 entity 的 viewmodel 應用
    //public interface IApiService<in TKey, TModel, out TDbContext, out TAppSettings, out TDefaultConnection, in TPrincipal> :
    public interface IApiService<in TKey, TModel, out TAppSettings, out TDefaultConnection, in TPrincipal> :
        IApiService<TKey, TModel, TPrincipal>,
        //IRepository<TDbContext, TAppSettings, TDefaultConnection>,
        IRepository<TAppSettings, TDefaultConnection>,
        ICurrentUser<TPrincipal>

        where TKey : IComparable<TKey>, IEquatable<TKey>//IComparable, IConvertible
        where TModel : ViewModel<TKey>
        //where TDbContext : class
        where TAppSettings : AppSettings<TDefaultConnection>, new()
        where TDefaultConnection : IDefaultConnection
    {
    }

    //public interface IApiService<in TKey, TModel, TEntity, in TPrincipal> : IApiService<TKey, TModel, TPrincipal>
    //     where TModel : class
    //{
    //    Task<TEntity> ReadAsync(TKey key);
    //}

    //  直接對應 entity 的 viewmodel 應用
    //public interface IApiService<in TKey, TModel, TEntity, out TDbContext, out TAppSettings, out TDefaultConnection, in TPrincipal> :
    public interface IApiService<in TKey, TModel, TEntity, out TAppSettings, out TDefaultConnection, in TPrincipal> :
        //IApiService<TKey, TModel, TEntity, TPrincipal>,
        IApiService<TKey, TModel, TPrincipal>,
        //IRepository<TDbContext, TAppSettings, TDefaultConnection>,
        IRepository<TAppSettings, TDefaultConnection>,
        ICurrentUser<TPrincipal>

        where TKey : IComparable<TKey>, IEquatable<TKey>//IComparable, IConvertible
        where TModel : ViewModel<TKey>, IViewModel<TModel, TEntity>
        //where TDbContext : class
        where TAppSettings : AppSettings<TDefaultConnection>, new()
        where TDefaultConnection : IDefaultConnection
    {
    }
}