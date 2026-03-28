namespace FoodDelivery.Application.Interfaces
{
    public interface IServiceAreaRepository
    {
        Task<Guid?> GetFirstActiveIdAsync(CancellationToken cancellationToken = default);
    }
}
