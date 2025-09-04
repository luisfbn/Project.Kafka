using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Kafka.Common.Configuration;
using Project.Kafka.Common.Exceptions;

namespace Project.Kafka.Common.Extensions
{
    public static class ConfigurationExtensions
    {

        public static IServiceCollection AddAppSettings(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<AppSettings>(config.GetSection("AppSettings"));

            return services ?? throw new ConfigurationMissingException("AppSettings");
        }
    }
}
