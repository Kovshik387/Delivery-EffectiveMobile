using AutoMapper;
using Delivery.Domain.Entities;
using Delivery.Services.OrderService.Data.Dto;

namespace Delivery.Services.OrderService.Data.Mapper;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order,OrderDto>().ReverseMap();
    }
}