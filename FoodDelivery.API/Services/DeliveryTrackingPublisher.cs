using FoodDelivery.API.Hubs;
using FoodDelivery.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace FoodDelivery.API.Services
{
    public class DeliveryTrackingPublisher : IDeliveryTrackingPublisher
    {
        private readonly IHubContext<TrackingHub> _hubContext;

        public DeliveryTrackingPublisher(IHubContext<TrackingHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task PublishDriverLocationAsync(
            Guid orderId,
            decimal latitude,
            decimal longitude,
            CancellationToken cancellationToken = default)
        {
            return _hubContext.Clients
                .Group(orderId.ToString())
                .SendAsync(
                    "LocationUpdated",
                    new { lat = latitude, lng = longitude },
                    cancellationToken);
        }
    }
}
