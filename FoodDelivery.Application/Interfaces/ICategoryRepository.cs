using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category);

        Task<List<Category>> GetByRestaurantAsync(Guid restaurantId);

        Task SaveChangesAsync();
    }
}
