namespace FoodDelivery.Application.Features.Delivery
{
    internal static class DeliveryDbTime
    {
        internal static DateTime UnspecifiedNow() =>
            DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
    }
}
