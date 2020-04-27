namespace Shengtai.Options
{
    public interface IAppSettings<out TAppSettings, out TDefaultConnection>
        where TAppSettings : AppSettings<TDefaultConnection>, new()
        where TDefaultConnection : IDefaultConnection
    {
        TAppSettings AppSettings { get; }
    }
}