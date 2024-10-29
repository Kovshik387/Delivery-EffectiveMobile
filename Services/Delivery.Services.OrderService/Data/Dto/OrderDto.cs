namespace Delivery.Services.OrderService.Data.Dto;

public class OrderDto
{
    public Guid OrderId { get; set; }
    public float Weight { get; set; } = default;
    public string District { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; } = DateTime.Now;
}