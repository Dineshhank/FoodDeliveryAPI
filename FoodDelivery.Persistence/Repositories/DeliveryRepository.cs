using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Persistence.Repositories
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public DeliveryRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Deliverypartner partner, CancellationToken cancellationToken = default)
        {
            await _context.Deliverypartners.AddAsync(partner, cancellationToken);
        }

        public async Task<Deliverypartner?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return await _context.Deliverypartners
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Userid == userId, cancellationToken);
        }

        public Task UpdateAsync(Deliverypartner partner, CancellationToken cancellationToken = default)
        {
            _context.Deliverypartners.Update(partner);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
