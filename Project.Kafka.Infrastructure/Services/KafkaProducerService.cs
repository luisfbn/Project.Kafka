using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Project.Kafka.Common.Configuration;
using Project.Kafka.Common.Constants;
using Project.Kafka.Common.DTOs;
using Project.Kafka.Core.Entity;
using Project.Kafka.Core.Interfaces;
using System.Text.Json;

namespace Project.Kafka.Infrastructure.Services;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly ILogger<KafkaProducerService> _logger;
    private readonly KafkaSettings? _settings;
    private readonly IProducer<Null, string> _producer;

    public KafkaProducerService(ILogger<KafkaProducerService> logger, IOptions<AppSettings> options)
    {
        _logger = logger;
        _settings = options.Value.Kafka;

        var producerConfig = new ProducerConfig
        {
            BootstrapServers = _settings?.BootstrapServers,
            SaslUsername = _settings?.SaslUsername,
            SaslPassword = _settings?.SaslPassword,
            SaslMechanism = SaslMechanism.Plain,
            SecurityProtocol = SecurityProtocol.SaslSsl
        };

        _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
    }

    public async Task SendOrderAsync(OrderDto orderDto)
    {
        var order = new Order
        {
            Product = orderDto.Product,
            Quantity = orderDto.Quantity,
            CreatedAt = DateTime.UtcNow
        };

        var json = JsonSerializer.Serialize(order);
        await _producer.ProduceAsync(KafkaTopics.Orders, new Message<Null, string> { Value = json });
        _logger.LogInformation("Order sent to Kafka: {Product} x{Qty}", order.Product, order.Quantity);
    }
}
