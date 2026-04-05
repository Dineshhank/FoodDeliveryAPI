using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace FoodDelivery.API.Hubs
{
    [Authorize]
    public class TrackingHub : Hub
    {
        public async Task JoinOrderTrackingGroup(string orderId)
        {
            var group = OrderTrackingGroupName.Normalize(orderId);
            if (string.IsNullOrEmpty(group))
                return;

            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task LeaveOrderTrackingGroup(string orderId)
        {
            var group = OrderTrackingGroupName.Normalize(orderId);
            if (string.IsNullOrEmpty(group))
                return;

            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        }
    }
}
