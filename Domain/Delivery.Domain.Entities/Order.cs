namespace Delivery.Domain.Entities;

public class Order
{
    public Guid OrderId { get; set; }
    public float Weight { get; set; }
    public string District { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.Now;
}