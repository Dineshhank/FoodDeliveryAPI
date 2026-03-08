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
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public RestaurantRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Restaurant restaurant)
        {
            await _context.Restaurants.AddAsync(restaurant);
        }

        public async Task<Restaurant?> GetByIdAsync(Guid id)
        {
            return await _context.Restaurants
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants
                .Where(x => x.Isdeleted == false)
                .ToListAsync();
        }

        public async Task UpdateAsync(Restaurant restaurant)
        {
            _context.Restaurants.Update(restaurant);
        }

        public async Task<List<(Restaurant Restaurant, double Distance)>>
        GetNearbyRestaurantsAsync(double lat, double lng, int radiusKm)
        {
            var restaurants = await _context.Restaurants
      .Where(r => r.Isactive == true && r.Isdeleted == false)
      .ToListAsync();

            var result = restaurants
                .Select(r =>
                {
                    var distance = CalculateDistance(
                        lat,
                        lng,
                        (double)r.Latitude,
                        (double)r.Longitude);

                    return (Restaurant: r, Distance: distance);
                })
                .Where(x => x.Distance <= radiusKm)
                .OrderBy(x => x.Distance)
                .ToList();

            return result;
        }

        private double CalculateDistance(
            double lat1,
            double lon1,
            double lat2,
            double lon2)
        {
            var R = 6371;

            var dLat = (lat2 - lat1) * Math.PI / 180;
            var dLon = (lon2 - lon1) * Math.PI / 180;

            var a =
                Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(lat1 * Math.PI / 180) *
                Math.Cos(lat2 * Math.PI / 180) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return R * c;
        }

        public async Task<List<Category>>
GetCategoriesByRestaurantAsync(Guid restaurantId)
        {
            return await _context.Categories
                .Where(x => x.Restaurantid == restaurantId)
                .OrderBy(x => x.Displayorder)
                .ToListAsync();
        }

        public async Task<List<Menuitem>>
        GetMenuItemsByRestaurantAsync(Guid restaurantId)
        {
            return await _context.Menuitems
                .Where(x => x.Restaurantid == restaurantId && x.Isdeleted == false)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
