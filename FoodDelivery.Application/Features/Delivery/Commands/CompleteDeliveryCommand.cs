using MediatR;

namespace FoodDelivery.Application.Features.Delivery.Commands
{
    public class CompleteDeliveryCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
    }
}
