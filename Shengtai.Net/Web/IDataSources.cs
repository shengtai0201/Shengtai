namespace Shengtai.Web
{
    public interface IDataSources<T>
    {
        T Build();

        IDataSources<T> Add(string key, string value);
    }
}