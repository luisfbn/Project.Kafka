using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Project.Kafka.Common.Configuration;
using System;
using Project.Kafka.Common.Constants;

namespace Project.Kafka.Infrastructure.Data;

public class OrderDbContextFactory //: IDesignTimeDbContextFactory<OrderDbContext>
{
    //public OrderDbContext CreateDbContext(string[] args)
    //{
    //    //var environment = builder.Environment.EnvironmentName;

    //    var config = new ConfigurationBuilder()
    //        .SetBasePath(Directory.GetCurrentDirectory())
    //        //.AddJsonFile("appsettings.json")
    //        .AddJsonFile($"appsettings.{Environments.Development}.json", optional: true)
    //        .Build();

    //    var dbSettings = config.GetRequiredSection("Database").Get<DbSettings>();

    //    var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();
    //    optionsBuilder.UseSqlite(dbSettings!.ConnectionString);

    //    // Le pasamos también el IOptions simulado
    //    var options = Microsoft.Extensions.Options.Options.Create(dbSettings);

    //    return new OrderDbContext(optionsBuilder.Options, options);
    //}

}
