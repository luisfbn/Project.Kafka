namespace Project.Kafka.Core.Entity
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
