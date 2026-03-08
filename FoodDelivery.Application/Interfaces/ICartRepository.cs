using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetUserCartByRestaurantAsync(Guid userId, Guid restaurantId);

        Task<Cartitem?> GetCartItemAsync(Guid cartId, Guid menuItemId);

        Task<Cart?> GetCartByUserIdAsync(Guid userId);

        Task<Cartitem?> GetCartItemByIdAsync(Guid cartItemId);

        Task RemoveCartItemAsync(Cartitem item);

        Task ClearCartAsync(Guid cartId);
        Task AddCartAsync(Cart cart);

        Task AddCartItemAsync(Cartitem item);

        Task UpdateCartItemAsync(Cartitem item);

        Task SaveChangesAsync();
    }
}
