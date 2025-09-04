using Project.Kafka.Common.DTOs;
using Project.Kafka.Core.Entity;

namespace Project.Kafka.Core.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order, CancellationToken cancellationToken);
        Task<List<OrderDto>> GetAllAsync();
    }

}
