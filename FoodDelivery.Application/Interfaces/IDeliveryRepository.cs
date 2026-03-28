using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Interfaces
{
    public interface IDeliveryRepository
    {
        Task AddAsync(Deliverypartner partner, CancellationToken cancellationToken = default);
        Task<Deliverypartner?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Deliverypartner partner, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
