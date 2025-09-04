using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Project.Kafka.Common.Configuration;
using Project.Kafka.Core.Interfaces;
using Project.Kafka.Infrastructure.Repositories;
using Project.Kafka.Infrastructure.Services;

namespace Project.Kafka.Infrastructure.DependencyInjection;

public static class InfrastructureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<OrderDbContext>((provider, options) =>
        {
            var appSettings = provider.GetRequiredService<IOptions<AppSettings>>().Value;
            options.UseSqlite(appSettings.Database.ConnectionString);
        });

        services.AddScoped<IOrderRepository, OrderRepository>();

        services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();

        services.AddScoped<IKafkaProducerService, KafkaProducerService>();

        return services;
    }
}