using FoodDelivery.Application.Features.Orders.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.Queries
{
    public class GetMyOrdersQuery: IRequest<List<OrderDto>>
    {
        public Guid UserId { get; set; }
    }
}
