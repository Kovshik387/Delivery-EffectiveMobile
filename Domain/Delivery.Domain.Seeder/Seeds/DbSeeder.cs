using Delivery.Domain.Context;
using Delivery.Domain.Context.Settings;
using Delivery.Domain.Seeder.Seeds.Demo;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Domain.Seeder.Seeds;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    }

    private static DeliveryContext DbContext(IServiceProvider serviceProvider)
    {
        return ServiceScope(serviceProvider)
            .ServiceProvider.GetRequiredService<IDbContextFactory<DeliveryContext>>().CreateDbContext();
    }

    public static async void Execute(IServiceProvider serviceProvider)
    {
        await AddDemoData(serviceProvider);
    }

    private static async Task AddDemoData(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null) return;

        var settings = scope.ServiceProvider.GetService<DbSettings>();
        if (!(settings!.InitSettings?.AddDemoData ?? false))
            return;
        
        await using var context = DbContext(serviceProvider);
        
        if (await context.Orders.AnyAsync())
            return;

        await context.Orders.AddRangeAsync(new DemoHelper().GetOrders());
        
        await context.SaveChangesAsync();
    }
}