using Delivery.Domain.Entities;
using Delivery.Services.OrderService.Data.Dto;
using Delivery.Services.OrderService.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Delivery.Systems.DeliveryAPI.Controllers;

[ApiController]
[Route("/api")]
public class DeliveryController : ControllerBase
{
    private readonly ILogger<DeliveryController> _logger;
    private readonly IOrderService _orderService;
    
    public DeliveryController(ILogger<DeliveryController> logger, IOrderService orderService)
    {
        _logger = logger; _orderService = orderService;
    }

    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> GetFilteredOrdersAsync([FromQuery] string cityDistrict,[FromQuery] DateTime time)
    {
        var result = await _orderService.FilterOrdersAsync(cityDistrict, time); 
        
        if (result.ErrorMessage.Equals(""))
            return BadRequest(result.ErrorMessage);
        
        return Ok(result);
    }

    [HttpGet]
    [Route("filter-file")]
    public async Task<IActionResult> GetFilteredOrderFilesAsync([FromQuery] string cityDistrict,[FromQuery] DateTime time)
    {
        var result = await _orderService.FilterOrdersAsync(cityDistrict, time);

        if (!result.ErrorMessage.Equals(""))
            return BadRequest(result.ErrorMessage);
        
        var tempFilePath = Path.GetTempFileName();
        await using (var writer = new StreamWriter(tempFilePath))
        {
            foreach (var order in result.Data!)
            {
                await writer.WriteLineAsync($"OrderId: {order.OrderId}, District: {order.District}, Weight: {order.Weight}, DeliveryTime: {order.OrderDate:yyyy-MM-dd HH:mm:ss}");
            }
        }

        const string fileName = "FilteredOrders.txt";
        return PhysicalFile(tempFilePath, "text/plain", fileName);
    }
    
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateOrderAsync([FromBody] OrderDto order)
    {
        return Ok(await _orderService.CreateOrderAsync(order));
    }
}