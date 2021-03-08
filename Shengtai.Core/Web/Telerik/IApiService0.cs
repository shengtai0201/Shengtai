using System.Threading.Tasks;

namespace Shengtai.Web.Telerik
{
    public interface IApiReadService<in TKey, TModel> where TModel : class
    {
        Task<TModel> ReadAsync(TKey key);

        Task<IDataSourceResponse<TModel>> ReadAsync(IDataSourceRequest request);
    }

    public interface IApiUpdateService<in TKey, TModel> : IApiReadService<TKey, TModel> where TModel : class
    {
        Task<bool?> UpdateAsync(TKey key, TModel model, IDataSource dataSource);
    }

    public interface IApiCreateDestroyService<in TKey, TModel> : IApiReadService<TKey, TModel> where TModel : class
    {
        Task<bool> CreateAsync(TModel model, IDataSource dataSource);

        Task<bool?> DestroyAsync(TKey key);
    }

    public interface IApiCreateUpdateService<in TKey, TModel> : IApiReadService<TKey, TModel> where TModel : class
    {
        Task<bool> CreateAsync(TModel model, IDataSource dataSource);

        Task<bool?> UpdateAsync(TKey key, TModel model, IDataSource dataSource);
    }

    public interface IApiService<in TKey, TModel> : IApiReadService<TKey, TModel> where TModel : class
    {
        Task<bool> CreateAsync(TModel model, IDataSource dataSource);

        Task<bool?> UpdateAsync(TKey key, TModel model, IDataSource dataSource);

        Task<bool?> DestroyAsync(TKey key);
    }
}
