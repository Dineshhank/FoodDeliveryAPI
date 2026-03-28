using FoodDelivery.Application.Features.Delivery;
using FoodDelivery.Application.Features.Delivery.Commands;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using MediatR;

namespace FoodDelivery.Application.Features.Delivery.Handlers
{
    public class AcceptOrderHandler : IRequestHandler<AcceptOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public AcceptOrderHandler(
            IOrderRepository orderRepository,
            IDeliveryRepository deliveryRepository)
        {
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
        }

        public async Task<bool> Handle(
            AcceptOrderCommand request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);
            if (order == null || order.Isdeleted == true)
                return false;

            if (order.Deliverypartnerid != null)
                return false;

            if (order.Status != DeliveryOrderStatuses.Confirmed)
                return false;

            var partner = await _deliveryRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (partner == null || partner.Isavailable != true)
                return false;

            var now = DeliveryDbTime.UnspecifiedNow();

            // orders.deliverypartnerid FK -> users.id (Order.Deliverypartner), not deliverypartners.id
            order.Deliverypartnerid = partner.Userid;
            order.Status = DeliveryOrderStatuses.Assigned;
            order.Updatedat = now;

            partner.Isavailable = false;
            partner.Lastactiveat = now;
            partner.Updatedat = now;

            await _orderRepository.UpdateOrderAsync(order);
            await _deliveryRepository.UpdateAsync(partner, cancellationToken);

            await _orderRepository.AddOrderStatusHistoryAsync(new Orderstatushistory
            {
                Id = Guid.NewGuid(),
                Orderid = order.Id,
                Status = DeliveryOrderStatuses.Assigned,
                Changedby = request.UserId,
                Changedat = now
            });

            await _orderRepository.SaveChangesAsync();

            return true;
        }
    }
}
