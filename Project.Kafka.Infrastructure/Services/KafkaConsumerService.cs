using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Project.Kafka.Common.Configuration;
using Project.Kafka.Common.Constants;
using Project.Kafka.Core.Entity;
using Project.Kafka.Core.Interfaces;
using System.Text.Json;

namespace Project.Kafka.Infrastructure.Services;

public class KafkaConsumerService : IKafkaConsumerService
{
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly KafkaSettings? _settings;
    private readonly IServiceProvider _provider;

    public KafkaConsumerService(
        ILogger<KafkaConsumerService> logger,
        IOptions<AppSettings> options,
        IServiceProvider provider)
    {
        _logger = logger;
        _settings = options.Value.Kafka;
        _provider = provider;
    }

    public async Task RunAsync(CancellationToken cancellationToken)
    {
        var kafkaConfig = new ConsumerConfig
        {
            BootstrapServers = _settings?.BootstrapServers,
            SaslUsername = _settings?.SaslUsername,
            SaslPassword = _settings?.SaslPassword,
            SaslMechanism = SaslMechanism.Plain,
            SecurityProtocol = SecurityProtocol.SaslSsl,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            GroupId = KafkaGroups.OrderId,
        };

        using var consumer = new ConsumerBuilder<Ignore, string>(kafkaConfig).Build();
        consumer.Subscribe(KafkaTopics.Orders);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                var result = consumer.Consume(cancellationToken);
                var order = JsonSerializer.Deserialize<Order>(result.Message.Value);

                if (order != null)
                {
                    using var scope = _provider.CreateScope();
                    var repo = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
                    await repo.AddAsync(order, cancellationToken);

                    _logger.LogInformation("Order saved: {Product} x{Qty} at {Time}",
                        order.Product, order.Quantity, order.CreatedAt);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consuming Kafka message");
            }
        }
    }
}
