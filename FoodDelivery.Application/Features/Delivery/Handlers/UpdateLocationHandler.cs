using FoodDelivery.Application.Features.Delivery;
using FoodDelivery.Application.Features.Delivery.Commands;
using FoodDelivery.Application.Interfaces;
using MediatR;

namespace FoodDelivery.Application.Features.Delivery.Handlers
{
    public class UpdateLocationHandler : IRequestHandler<UpdateLocationCommand, bool>
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryTrackingPublisher _trackingPublisher;

        public UpdateLocationHandler(
            IDeliveryRepository deliveryRepository,
            IOrderRepository orderRepository,
            IDeliveryTrackingPublisher trackingPublisher)
        {
            _deliveryRepository = deliveryRepository;
            _orderRepository = orderRepository;
            _trackingPublisher = trackingPublisher;
        }

        public async Task<bool> Handle(
            UpdateLocationCommand request,
            CancellationToken cancellationToken)
        {
            var partner = await _deliveryRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (partner == null)
                return false;

            var now = DeliveryDbTime.UnspecifiedNow();
            partner.Currentlatitude = request.Latitude;
            partner.Currentlongitude = request.Longitude;
            partner.Lastactiveat = now;
            partner.Updatedat = now;

            await _deliveryRepository.UpdateAsync(partner, cancellationToken);
            await _deliveryRepository.SaveChangesAsync(cancellationToken);

            if (request.ActiveOrderId is { } activeOrderId)
            {
                var order = await _orderRepository.GetOrderByIdAsync(activeOrderId);
                if (order != null && order.Deliverypartnerid == partner.Userid)
                {
                    await _trackingPublisher.PublishDriverLocationAsync(
                        activeOrderId,
                        request.Latitude,
                        request.Longitude,
                        cancellationToken);
                }
            }

            return true;
        }
    }
}
