using Project.Kafka.Common.Extensions;
using Project.Kafka.Infrastructure.DependencyInjection;

namespace Project.Kafka.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    var config = context.Configuration;
                    services.AddAppSettings(config);
                    services.AddInfrastructure(config);
                    services.AddHostedService<Worker>();
                })
                .Build()
                .Run();
        }
    }
}