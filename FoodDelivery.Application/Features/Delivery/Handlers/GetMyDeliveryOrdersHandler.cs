using System.Collections.Generic;
using System.Linq;
using FoodDelivery.Application.Features.Delivery.Queries;
using FoodDelivery.Application.Features.Orders.DTOs;
using FoodDelivery.Application.Interfaces;
using MediatR;

namespace FoodDelivery.Application.Features.Delivery.Handlers
{
    public class GetMyDeliveryOrdersHandler : IRequestHandler<GetMyDeliveryOrdersQuery, List<OrderDto>>
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IOrderRepository _orderRepository;

        public GetMyDeliveryOrdersHandler(
            IDeliveryRepository deliveryRepository,
            IOrderRepository orderRepository)
        {
            _deliveryRepository = deliveryRepository;
            _orderRepository = orderRepository;
        }

        public async Task<List<OrderDto>> Handle(
            GetMyDeliveryOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var partner = await _deliveryRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (partner == null)
                return new List<OrderDto>();

            // orders.deliverypartnerid stores the rider's user id
            var orders = await _orderRepository.GetOrdersByDeliveryPartnerIdAsync(
                partner.Userid,
                cancellationToken);

            return orders.Select(order => new OrderDto
            {
                OrderId = order.Id,
                OrderNumber = order.Ordernumber,
                FinalAmount = order.Finalamount,
                Status = order.Status,
                CreatedAt = order.Createdat ?? default,
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
