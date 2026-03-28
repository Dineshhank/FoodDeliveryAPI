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
    public class GetMyOrdersHandler : IRequestHandler<GetMyOrdersQuery, List<OrderDto>>
    {
        private readonly IOrderRepository _repo;

        public GetMyOrdersHandler(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<OrderDto>> Handle(GetMyOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _repo.GetOrdersByUserIdAsync(request.UserId);

            return orders.Select(order => new OrderDto
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
            }).ToList();
        }
    }
}
