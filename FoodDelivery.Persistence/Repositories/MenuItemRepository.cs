using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Persistence.Repositories
{
    public class MenuItemRepository: IMenuItemRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public MenuItemRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Menuitem item)
        {
            await _context.Menuitems.AddAsync(item);
        }

        public async Task<List<Menuitem>> GetByRestaurantAsync(Guid restaurantId)
        {
            return await _context.Menuitems
                .Where(x => x.Restaurantid == restaurantId && x.Isdeleted == false)
                .ToListAsync();
        }

        public async Task<Menuitem?> GetByIdAsync(Guid id)
        {
            return await _context.Menuitems
                .Where(x => x.Id == id && x.Isdeleted == false).FirstOrDefaultAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
