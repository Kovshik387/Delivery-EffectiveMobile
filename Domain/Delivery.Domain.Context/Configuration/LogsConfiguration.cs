using Delivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Domain.Context.Configuration;

public static class LogsConfiguration
{
    public static void ConfigureLogs(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.LogId);
            
            entity.ToTable("logs");
            
            entity.Property(e => e.LogId)
                .HasColumnName("id");
            
            entity.Property(e => e.Type)
                .HasColumnName("type");
            
            entity.Property(e => e.Message)
                .HasColumnName("message");

            entity.Property(e => e.Date)
                .HasColumnName("date");
        });
    }
}