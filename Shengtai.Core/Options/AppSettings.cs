namespace Shengtai.Options
{
    public class AppSettings<TDefaultConnection>
        where TDefaultConnection : IDefaultConnection
    {
        public TDefaultConnection ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
    }
}