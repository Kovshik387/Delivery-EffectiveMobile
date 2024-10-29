using Microsoft.EntityFrameworkCore;

namespace Delivery.Domain.Context.Factories;

public class DbContextFactory
{
    private readonly DbContextOptions<DeliveryContext> _options;

    public DbContextFactory(DbContextOptions<DeliveryContext> options)
    {
        this._options = options;
    }

    public DeliveryContext Create()
    {
        return new DeliveryContext(_options);
    }
}