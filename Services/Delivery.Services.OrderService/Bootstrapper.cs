using Delivery.Services.OrderService.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Services.OrderService;

public static class Bootstrapper
{
    public static IServiceCollection AddOrderService(this IServiceCollection services)
    {
        return services.AddTransient<IOrderService, Services.OrderService>();
    }
}