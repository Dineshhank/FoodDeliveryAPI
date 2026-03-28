using System.Collections.Generic;
using FoodDelivery.Application.Features.Delivery.DTOs;
using MediatR;

namespace FoodDelivery.Application.Features.Delivery.Queries
{
    public class GetAvailableDeliveryOrdersQuery : IRequest<List<AvailableDeliveryOrderDto>>
    {
        public Guid UserId { get; set; }
    }
}
