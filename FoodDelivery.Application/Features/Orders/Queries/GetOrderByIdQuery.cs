using FoodDelivery.Application.Features.Orders.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.Queries
{
    public class GetOrderByIdQuery : IRequest<OrderDto>
    {
        public Guid OrderId { get; set; }
    }   

}
