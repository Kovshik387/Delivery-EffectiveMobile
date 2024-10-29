using AutoMapper;
using Delivery.Domain.Context;
using Delivery.Domain.Entities;
using Delivery.Services.OrderService.Data.Dto;
using Delivery.Services.OrderService.Data.Responses;
using Delivery.Services.OrderService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Delivery.Services.OrderService.Services;
/// <summary>
/// Реализация <see cref="IOrderService"/> бизнес-логики заказов
/// </summary>
public class OrderService : IOrderService
{
    private readonly ILogger<OrderService> _logger;
    private readonly DeliveryContext _deliveryContext;
    private readonly IMapper _mapper;
    
    public OrderService(ILogger<OrderService> logger, DeliveryContext deliveryContext, IMapper mapper)
    {
        _logger = logger; _deliveryContext = deliveryContext;
        _mapper = mapper;
    }
    
    public async Task<OrderResponse<bool>> CreateOrderAsync(OrderDto order)
    {
        try
        {
            if (order.Weight <= 0 || string.IsNullOrEmpty(order.District))
            {
                await DbLog($"Некорректные данные: {order.Weight} {order.District}",true);
                return new OrderResponse<bool>()
                {
                    Data = false,
                    ErrorMessage = "Некорректные данные"
                };
            }
            
            _deliveryContext.Orders.Add(_mapper.Map<OrderDto, Order>(order));
            await _deliveryContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            await DbLog("Не удалось создать объект",true);
            return new OrderResponse<bool>()
            {
                Data = false,
                ErrorMessage = "Что-то пошло не так"
            };
        }

        return new OrderResponse<bool>()
        {
            Data = true,
        };
    }
    /// <summary>
    /// Фильтрация заказов по заданным критериям
    /// </summary>
    /// <param name="cityDistrict">район</param>
    /// <param name="startDate">время заказа</param>
    /// <returns></returns>
    public async Task<OrderResponse<List<OrderDto>>> FilterOrdersAsync(string cityDistrict, DateTime startDate)
    {
        if (!_deliveryContext.Orders.Any(x => x.District.Equals(cityDistrict.ToLower())))
        {
            await DbLog($"Некорректно указан район: {cityDistrict}",true);
            return new OrderResponse<List<OrderDto>>()
            {
                Data = null,
                ErrorMessage = "Некорректно указан район"
            };
        }
        //TODO возможность выбора конечного времени
        var endDate = DateTime.UtcNow;
        
        var result = await _deliveryContext.Orders
            .Where(x => x.District.Equals(cityDistrict.ToLower()) &&
                        x.OrderDate >= startDate.ToUniversalTime() &&
                        x.OrderDate <= endDate)
            .ToListAsync();

        await DbLog($"Найдено {result.Count} заказов");
        
        return new OrderResponse<List<OrderDto>>()
        {
            Data = _mapper.Map<List<OrderDto>>(result),
            ErrorMessage = ""
        };
    }

    private async Task DbLog(string message, bool isError = false)
    {
        _logger.LogInformation(message);
        _deliveryContext.Logs.Add(new Delivery.Domain.Entities.Log()
        {
            Message = message,
            Type = isError ? "Ошибка" : "Успех", 
            Date = DateTime.UtcNow
            
        });
        
        await _deliveryContext.SaveChangesAsync();
    }
    
}