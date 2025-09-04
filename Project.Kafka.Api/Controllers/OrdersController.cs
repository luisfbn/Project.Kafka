using Microsoft.AspNetCore.Mvc;
using Project.Kafka.Common.DTOs;
using Project.Kafka.Core.Interfaces;

namespace Project.Kafka.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController(IKafkaProducerService producer, IOrderRepository repository) : Controller
    {
        private readonly IKafkaProducerService _producer = producer;
        private readonly IOrderRepository _repository = repository;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderDto orderDto)
        {
            await _producer.SendOrderAsync(orderDto);
            return Ok(new { status = "Order sent to Kafka" });
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderDto>>> Get()
        {
            return await _repository.GetAllAsync();
        }

    }
}
