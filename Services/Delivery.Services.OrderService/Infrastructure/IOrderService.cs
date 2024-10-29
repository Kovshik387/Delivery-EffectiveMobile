using Delivery.Services.OrderService.Data.Dto;
using Delivery.Services.OrderService.Data.Responses;

namespace Delivery.Services.OrderService.Infrastructure;
/// <summary>
/// Представляет сервис бизнес-логики заказов
/// </summary>
public interface IOrderService
{
    public Task<OrderResponse<bool>> CreateOrderAsync(OrderDto order);
    public Task<OrderResponse<List<OrderDto>>> FilterOrdersAsync(string cityDistrict, DateTime startDate);
}