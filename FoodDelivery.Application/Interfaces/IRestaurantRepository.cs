using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Interfaces
{
    public interface IRestaurantRepository
    {
        Task AddAsync(Restaurant restaurant);

        Task<Restaurant?> GetByIdAsync(Guid id);

        Task<List<Restaurant>> GetAllAsync();

        Task UpdateAsync(Restaurant restaurant);

        Task<List<(Domain.Entities.Restaurant Restaurant, double Distance)>>
        GetNearbyRestaurantsAsync(double latitude, double longitude, int radiusKm);

        Task<List<Category>> GetCategoriesByRestaurantAsync(Guid restaurantId);

        Task<List<Menuitem>> GetMenuItemsByRestaurantAsync(Guid restaurantId);

        Task SaveChangesAsync();
    }
}
