using System.Threading.Tasks;

namespace Shengtai.Web.Telerik
{
    public interface IApiUpdateService<in TKey, TModel> where TModel : class
    {
        Task<TModel> ReadAsync(TKey key);

        Task<IDataSourceResponse<TModel>> ReadAsync(IDataSourceRequest request);

        Task<bool?> UpdateAsync(TKey key, TModel model, IDataSource dataSource);
    }

    public interface IApiService<in TKey, TModel> : IApiUpdateService<TKey, TModel> where TModel : class
    {
        Task<bool> CreateAsync(TModel model, IDataSource dataSource);

        Task<bool?> DestroyAsync(TKey key);
    }
}
