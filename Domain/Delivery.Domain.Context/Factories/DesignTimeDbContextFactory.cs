using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Delivery.Domain.Context.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DeliveryContext>
{
    public DeliveryContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.context.json"), false)
            .Build();
        
        var connectionString = configuration.GetConnectionString($"PgSql");

        var options = DbContextOptionsFactory.Create(connectionString!, false);
        var factory = new DbContextFactory(options);
        var context = factory.Create();
        
        return context;
    }
}