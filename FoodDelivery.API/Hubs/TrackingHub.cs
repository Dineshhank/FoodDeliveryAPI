using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FoodDelivery.API.Hubs
{
    [Authorize]
    public class TrackingHub : Hub
    {
        public async Task JoinOrderTrackingGroup(string orderId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, orderId);
        }

        public async Task LeaveOrderTrackingGroup(string orderId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, orderId);
        }
    }
}
