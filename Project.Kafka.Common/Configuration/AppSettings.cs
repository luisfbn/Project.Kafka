using System.ComponentModel.DataAnnotations;

namespace Project.Kafka.Common.Configuration
{
    public class AppSettings
    {
        [Required]
        public string Environment { get; set; } = string.Empty;

        [Required]
        public KafkaSettings Kafka { get; set; } = new();

        [Required]
        public DbSettings Database { get; set; } = new();
    }
}
