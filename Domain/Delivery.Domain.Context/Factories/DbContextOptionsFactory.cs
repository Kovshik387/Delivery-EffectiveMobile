using Microsoft.EntityFrameworkCore;

namespace Delivery.Domain.Context.Factories;

public class DbContextOptionsFactory
{
    private const string MigrationProjectPrefix = "Delivery.Domain.Migrations.";

    public static DbContextOptions<DeliveryContext> Create(string connectionString,bool detailedLogging = false)
    {
        var builder = new DbContextOptionsBuilder<DeliveryContext>();
        
        Configure(connectionString, detailedLogging).Invoke(builder);
        
        return builder.Options;
    }

    public static Action<DbContextOptionsBuilder> Configure(string connectionString, bool detailedLogging = false)
    {
        return (builder) =>
        {
            builder.UseNpgsql(connectionString,
                options => options
                    .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                    .MigrationsAssembly(MigrationProjectPrefix)
                    .MigrationsAssembly($"{typeof(DbContextOptionsFactory).Assembly.GetName().Name}")
            );
            
            if (detailedLogging) { builder.EnableDetailedErrors(); }
        };
    }
}