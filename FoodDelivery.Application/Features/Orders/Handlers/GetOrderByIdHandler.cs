using FoodDelivery.Application.Features.Orders.DTOs;
using FoodDelivery.Application.Features.Orders.Queries;
using FoodDelivery.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.Handlers
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IOrderRepository _repo;

        public GetOrderByIdHandler(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _repo.GetOrderByIdAsync(request.OrderId);

            if (order == null)
                return null;

            return new OrderDto
            {
                OrderId = order.Id,
                OrderNumber = order.Ordernumber,
                FinalAmount = order.Finalamount,
                Status = order.Status,
              
                Items = order.Orderitems.Select(i => new OrderItemDto
                {
                    MenuItemId = i.Menuitemid,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };
        }
    }
}
