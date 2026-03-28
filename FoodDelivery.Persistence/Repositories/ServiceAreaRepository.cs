using FoodDelivery.Application.Interfaces;
using FoodDelivery.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Persistence.Repositories
{
    public class ServiceAreaRepository : IServiceAreaRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public ServiceAreaRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task<Guid?> GetFirstActiveIdAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Serviceareas
                .AsNoTracking()
                .Where(s => s.Isactive && !s.Isdeleted)
                .OrderBy(s => s.Createdat)
                .Select(s => (Guid?)s.Id)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
