namespace Shengtai.Options
{
    public interface IAppSettings<out TAppSettings, out TDefaultConnection>
        where TAppSettings : AppSettings<TDefaultConnection>, new()
        where TDefaultConnection : IConnectionStrings
    {
        TAppSettings AppSettings { get; }
    }

    public interface IAppSettings<TConnectionStrings> where TConnectionStrings : IConnectionStrings
    {
        TConnectionStrings ConnectionStrings { get; }
    }
}