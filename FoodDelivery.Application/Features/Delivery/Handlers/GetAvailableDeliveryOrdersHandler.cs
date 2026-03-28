using System.Collections.Generic;
using System.Linq;
using FoodDelivery.Application.Features.Delivery.DTOs;
using FoodDelivery.Application.Features.Delivery.Queries;
using FoodDelivery.Application.Features.Orders.DTOs;
using FoodDelivery.Application.Interfaces;
using MediatR;

namespace FoodDelivery.Application.Features.Delivery.Handlers
{
    public class GetAvailableDeliveryOrdersHandler
        : IRequestHandler<GetAvailableDeliveryOrdersQuery, List<AvailableDeliveryOrderDto>>
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IOrderRepository _orderRepository;

        public GetAvailableDeliveryOrdersHandler(
            IDeliveryRepository deliveryRepository,
            IOrderRepository orderRepository)
        {
            _deliveryRepository = deliveryRepository;
            _orderRepository = orderRepository;
        }

        public async Task<List<AvailableDeliveryOrderDto>> Handle(
            GetAvailableDeliveryOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var partner = await _deliveryRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (partner == null || partner.Isavailable != true)
                return new List<AvailableDeliveryOrderDto>();

            var orders = await _orderRepository.GetAvailableOrdersForDeliveryAsync(
                partner.Serviceareaid,
                cancellationToken);

            return orders.Select(o => new AvailableDeliveryOrderDto
            {
                OrderId = o.Id,
                OrderNumber = o.Ordernumber,
                FinalAmount = o.Finalamount,
                Status = o.Status,
                CreatedAt = o.Createdat ?? default,
                DeliveryAddress = o.Deliveryaddress,
                DeliveryLatitude = o.Deliverylatitude,
                DeliveryLongitude = o.Deliverylongitude,
                RestaurantId = o.Restaurantid,
                RestaurantName = o.Restaurant.Name,
                RestaurantLatitude = o.Restaurantlatitude ?? o.Restaurant.Latitude,
                RestaurantLongitude = o.Restaurantlongitude ?? o.Restaurant.Longitude,
                RestaurantAddress = o.Restaurant.Address,
                Items = o.Orderitems.Select(i => new OrderItemDto
                {
                    MenuItemId = i.Menuitemid,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            }).ToList();
        }
    }
}
