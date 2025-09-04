namespace Project.Kafka.Core.Interfaces;

public interface IKafkaConsumerService
{
    Task RunAsync(CancellationToken cancellationToken);
}

