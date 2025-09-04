using Microsoft.EntityFrameworkCore;
using Project.Kafka.Common.DTOs;
using Project.Kafka.Core.Entity;
using Project.Kafka.Core.Interfaces;

namespace Project.Kafka.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _dbContext;

    public OrderRepository(OrderDbContext context)
    {
        _dbContext = context;
    }

    public async Task AddAsync(Order order, CancellationToken cancellationToken)
    {
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<OrderDto>> GetAllAsync()
    {
        var orders = await _dbContext.Orders
        .OrderByDescending(o => o.CreatedAt)
        .Select(o => new OrderDto
        {
            Product = o.Product,
            Quantity = o.Quantity,
        })
        .ToListAsync();

        return orders;
    }
}
