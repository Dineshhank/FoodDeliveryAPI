using FoodDelivery.Application.Features.Orders.DTOs;
using MediatR;
using System;

namespace FoodDelivery.Application.Features.Orders.Queries
{
    public class GetMyActiveOrderQuery : IRequest<CustomerActiveOrderDetailDto?>
    {
        public Guid UserId { get; set; }
    }
}
