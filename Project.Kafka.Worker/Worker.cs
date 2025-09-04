using Project.Kafka.Core.Interfaces;

namespace Project.Kafka.Worker
{
    public class Worker : BackgroundService
    {
        private readonly IKafkaConsumerService _consumer;

        public Worker(IKafkaConsumerService consumer)
        {
            _consumer = consumer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _consumer.RunAsync(stoppingToken);
        }
    }
}
