namespace FoodDelivery.Application.Features.Delivery
{
    internal static class DeliveryOrderStatuses
    {
        internal const string Confirmed = "CONFIRMED";
        internal const string Assigned = "Assigned";
        internal const string ReachedRestaurant = "ReachedRestaurant";
        internal const string PickedUp = "PickedUp";
        internal const string OnTheWayToCustomer = "OnTheWayToCustomer";
        internal const string Delivered = "Delivered";
    }
}
