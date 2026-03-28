namespace FoodDelivery.Application.Features.Delivery.DTOs
{
    public class UpdateLocationRequest
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        /// <summary>When set, customers joined to this order group receive live location via SignalR.</summary>
        public Guid? ActiveOrderId { get; set; }
    }
}
