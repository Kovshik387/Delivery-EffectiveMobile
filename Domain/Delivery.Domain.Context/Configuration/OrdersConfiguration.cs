using Delivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Domain.Context.Configuration;

public static class OrdersConfiguration
{
    public static void ConfigureOrders(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId);
            
            entity.ToTable("orders");
            
            entity.Property(e => e.OrderId)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            
            entity.Property(e => e.OrderDate)
                .HasColumnName("order_date");
            
            entity.Property(e => e.Weight)
                .HasColumnName("weight");
            
            entity.Property(e => e.District)
                .HasColumnName("district")
                .HasMaxLength(50);
        });
    }
}