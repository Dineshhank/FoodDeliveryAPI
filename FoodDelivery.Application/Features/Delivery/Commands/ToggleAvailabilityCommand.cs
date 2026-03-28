using MediatR;

namespace FoodDelivery.Application.Features.Delivery.Commands
{
    public class ToggleAvailabilityCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
