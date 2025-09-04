using Project.Kafka.Common.DTOs;

namespace Project.Kafka.Core.Interfaces
{
    public interface IKafkaProducerService
    {
        Task SendOrderAsync(OrderDto order);
    }
}
