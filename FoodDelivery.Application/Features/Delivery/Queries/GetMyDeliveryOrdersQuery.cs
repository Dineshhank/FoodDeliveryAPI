using System.Collections.Generic;
using FoodDelivery.Application.Features.Orders.DTOs;
using MediatR;

namespace FoodDelivery.Application.Features.Delivery.Queries
{
    public class GetMyDeliveryOrdersQuery : IRequest<List<OrderDto>>
    {
        public Guid UserId { get; set; }
    }
}
