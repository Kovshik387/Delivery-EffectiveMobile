namespace Delivery.Services.OrderService.Data.Responses;

public class OrderResponse<TData>
{
    public TData? Data { get; set; } = default!;
    public string ErrorMessage { get; set; } = string.Empty;
}