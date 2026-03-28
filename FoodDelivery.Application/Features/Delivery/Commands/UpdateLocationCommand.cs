using MediatR;

namespace FoodDelivery.Application.Features.Delivery.Commands
{
    public class UpdateLocationCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public Guid? ActiveOrderId { get; set; }
    }
}
