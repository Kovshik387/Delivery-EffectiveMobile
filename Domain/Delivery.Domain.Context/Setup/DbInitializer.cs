using Delivery.Domain.Context.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Domain.Context.Setup;

public static class DbInitializer
{
    public static void Execute(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
        ArgumentNullException.ThrowIfNull(scope);
        
        var dbContext = scope.ServiceProvider.GetRequiredService<IDbContextFactory<DeliveryContext>>();
        using var context = dbContext.CreateDbContext();
        context.Database.Migrate();
    }
}