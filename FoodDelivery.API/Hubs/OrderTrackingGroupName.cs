using System;

namespace FoodDelivery.API.Hubs
{
    /// <summary>
    /// SignalR group names are case-sensitive. Clients may send order ids with different casing;
    /// server publishes using <see cref="Guid.ToString()"/>. Normalize so join and publish hit the same group.
    /// </summary>
    internal static class OrderTrackingGroupName
    {
        public static string Normalize(string? orderId)
        {
            if (string.IsNullOrWhiteSpace(orderId))
                return string.Empty;

            var trimmed = orderId.Trim();
            return Guid.TryParse(trimmed, out var g)
                ? g.ToString("D")
                : trimmed.ToLowerInvariant();
        }

        public static string From(Guid orderId) => orderId.ToString("D");
    }
}
