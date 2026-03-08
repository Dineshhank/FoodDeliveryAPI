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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public CategoryRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        public async Task<List<Category>> GetByRestaurantAsync(Guid restaurantId)
        {
            return await _context.Categories
                .Where(x => x.Restaurantid == restaurantId)
                .OrderBy(x => x.Displayorder)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
