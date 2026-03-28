using FoodDelivery.Application.Features.Delivery;
using FoodDelivery.Application.Features.Delivery.Commands;
using FoodDelivery.Application.Interfaces;
using MediatR;

namespace FoodDelivery.Application.Features.Delivery.Handlers
{
    public class ToggleAvailabilityHandler : IRequestHandler<ToggleAvailabilityCommand, bool>
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public ToggleAvailabilityHandler(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<bool> Handle(
            ToggleAvailabilityCommand request,
            CancellationToken cancellationToken)
        {
            var partner = await _deliveryRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if (partner == null)
                return false;

            var now = DeliveryDbTime.UnspecifiedNow();
            partner.Isavailable = request.IsAvailable;
            partner.Lastactiveat = now;
            partner.Updatedat = now;

            await _deliveryRepository.UpdateAsync(partner, cancellationToken);
            await _deliveryRepository.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
