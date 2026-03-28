namespace FoodDelivery.Application.Interfaces
{
    public interface IDeliveryTrackingPublisher
    {
        Task PublishDriverLocationAsync(
            Guid orderId,
            decimal latitude,
            decimal longitude,
            CancellationToken cancellationToken = default);
    }
}
