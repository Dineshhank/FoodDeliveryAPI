using FoodDelivery.Application.Features.Delivery;
using FoodDelivery.Application.Features.Delivery.Commands;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.Application.Features.Delivery.Handlers
{
    public class CompleteDeliveryHandler : IRequestHandler<CompleteDeliveryCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public CompleteDeliveryHandler(
            IOrderRepository orderRepository,
            IDeliveryRepository deliveryRepository)
        {
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
        }

        public async Task<bool> Handle(
            CompleteDeliveryCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
            if (order == null || order.Isdeleted == true)
                return false;

            var partner = await _deliveryRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (partner == null || order.Deliverypartnerid != partner.Userid)
                return false;

            if (order.Status != DeliveryOrderStatuses.OnTheWayToCustomer)
                return false;

            if (!DeliveryLocationRules.DriverNearCustomer(partner, order, DeliveryGeofence.DefaultMeters))
                return false;

            var now = DeliveryDbTime.UnspecifiedNow();

            order.Status = DeliveryOrderStatuses.Delivered;
            order.Deliveredat = now;
            order.Updatedat = now;

            partner.Isavailable = true;
            partner.Lastactiveat = now;
            partner.Updatedat = now;
            partner.Totaldeliveries = (partner.Totaldeliveries ?? 0) + 1;

            await _orderRepository.UpdateOrderAsync(order);
            await _deliveryRepository.UpdateAsync(partner, cancellationToken);

            await _orderRepository.AddOrderStatusHistoryAsync(new Orderstatushistory
            {
                Id = Guid.NewGuid(),
                Orderid = order.Id,
                Status = DeliveryOrderStatuses.Delivered,
                Changedby = request.UserId,
                Changedat = now
            });

            await _orderRepository.SaveChangesAsync();

            return true;
        }
    }
}
