using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Shengtai.Options
{
    public class AppSettings<TDefaultConnection>
        where TDefaultConnection : IConnectionStrings
    {
        public TDefaultConnection ConnectionStrings { get; set; }
        public Logging Logging { get; set; }
    }

    public abstract class AppSettings<TAppSettings, TConnectionStrings> : IAppSettings<TConnectionStrings>
        where TAppSettings : class, IAppSettings<TConnectionStrings>
        where TConnectionStrings : IConnectionStrings
    {
        protected AppSettings()
        {
            ConnectionStrings = Activator.CreateInstance<TConnectionStrings>();
        }

        public TConnectionStrings ConnectionStrings { get; set; }

        public static TAppSettings AddSingleton(IServiceCollection services, IConfiguration configuration, Action<TAppSettings> action = null)
        {
            var appSettings = Activator.CreateInstance<TAppSettings>();
            configuration.Bind(appSettings);

            services.AddSingleton(appSettings);
            services.AddSingleton<IConnectionStrings>(appSettings.ConnectionStrings);

            action?.Invoke(appSettings);

            return appSettings;
        }
    }
}