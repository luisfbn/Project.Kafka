using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project.Kafka.Common.Configuration;
using Project.Kafka.Core.Entity;

public class OrderDbContext : DbContext
{
    private readonly DbSettings _dbSettings;

    public OrderDbContext(DbContextOptions<OrderDbContext> options, IOptions<AppSettings> dbOptions)
        : base(options)
    {
        _dbSettings = dbOptions.Value.Database;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Solo se usa si no se configura en tiempo de ejecución (ej. durante migraciones CLI)
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite(_dbSettings.ConnectionString);
        }
    }

    public DbSet<Order> Orders { get; set; }
}

