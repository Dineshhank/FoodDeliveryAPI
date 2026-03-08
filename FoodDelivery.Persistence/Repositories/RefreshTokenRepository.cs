using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDelivery.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace FoodDelivery.Persistence.Repositories
{
    public class RefreshTokenRepository :IRefreshTokenRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public RefreshTokenRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Refreshtoken token)
        {
            await _context.Refreshtokens.AddAsync(token);
        }

        public async Task<Refreshtoken?> GetByTokenAsync(string token)
        {
            return await _context.Refreshtokens
                .FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
