using FoodDelivery.Application.Features.Delivery;
using FoodDelivery.Application.Features.Delivery.Commands;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.Application.Features.Delivery.Handlers
{
    public class PickupOrderHandler : IRequestHandler<PickupOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public PickupOrderHandler(
            IOrderRepository orderRepository,
            IDeliveryRepository deliveryRepository)
        {
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
        }

        public async Task<bool> Handle(
            PickupOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
            if (order == null || order.Isdeleted == true)
                return false;

            var partner = await _deliveryRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (partner == null || order.Deliverypartnerid != partner.Userid)
                return false;

            if (order.Status != DeliveryOrderStatuses.ReachedRestaurant)
                return false;

            if (!DeliveryLocationRules.DriverNearRestaurant(partner, order, DeliveryGeofence.DefaultMeters))
                return false;

            var now = DeliveryDbTime.UnspecifiedNow();
            order.Status = DeliveryOrderStatuses.PickedUp;
            order.Updatedat = now;

            await _orderRepository.UpdateOrderAsync(order);

            await _orderRepository.AddOrderStatusHistoryAsync(new Orderstatushistory
            {
                Id = Guid.NewGuid(),
                Orderid = order.Id,
                Status = DeliveryOrderStatuses.PickedUp,
                Changedby = request.UserId,
                Changedat = now
            });

            await _orderRepository.SaveChangesAsync();

            return true;
        }
    }
}
