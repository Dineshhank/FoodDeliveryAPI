using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Interfaces
{
    public interface IMenuItemRepository
    {
        Task AddAsync(Menuitem item);

        Task<List<Menuitem>> GetByRestaurantAsync(Guid restaurantId);
        Task<Menuitem?> GetByIdAsync(Guid id);
        Task SaveChangesAsync();
    }
}
