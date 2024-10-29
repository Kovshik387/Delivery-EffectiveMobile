using Delivery.Shared.Common.Helpers;

namespace Delivery.Systems.DeliveryAPI.Configuration;

public static class AutoMapperConfiguration
{
    public static IServiceCollection AddAppAutoMapper(this IServiceCollection services)
    {
        AutoMapperHelper.Register(services);
        return services;
    }
}