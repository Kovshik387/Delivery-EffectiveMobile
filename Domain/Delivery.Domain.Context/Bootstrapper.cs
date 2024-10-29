using Delivery.Domain.Context.Factories;
using Delivery.Domain.Context.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Delivery.Domain.Context;

public static class Bootstrapper
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var dbSetting = configuration.GetSection(DbSettings.SectionName).Get<DbSettings>();

        if (dbSetting == null) return serviceCollection;
        
        serviceCollection.AddSingleton(dbSetting);
        
        var dbInitDelegate = DbContextOptionsFactory.Configure(dbSetting!.ConnectionString, false);
        return serviceCollection.AddDbContextFactory<DeliveryContext>(dbInitDelegate);
    }
}