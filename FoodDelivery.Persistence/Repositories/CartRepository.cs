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
    public class CartRepository : ICartRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public CartRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
        {
            return await _context.Carts
                .Include(x => x.Cartitems)
                .FirstOrDefaultAsync(x => x.Userid == userId);
        }

        public Task RemoveCartItemAsync(Cartitem item)
        {
            _context.Cartitems.Remove(item);
            return Task.CompletedTask;
        }

        public async Task<Cartitem?> GetCartItemByIdAsync(Guid cartItemId)
        {
            return await _context.Cartitems
                .FirstOrDefaultAsync(x => x.Id == cartItemId);
        }

        public async Task ClearCartAsync(Guid cartId)
        {
            var items = _context.Cartitems.Where(x => x.Cartid == cartId);

            _context.Cartitems.RemoveRange(items);

            await Task.CompletedTask;
        }
        public async Task<Cart?> GetUserCartByRestaurantAsync(Guid userId, Guid restaurantId)
        {
            return await _context.Carts
                .Include(x => x.Cartitems)
                .FirstOrDefaultAsync(x =>
                    x.Userid == userId &&
                    x.Restaurantid == restaurantId);
        }

        public async Task<Cartitem?> GetCartItemAsync(Guid cartId, Guid menuItemId)
        {
            return await _context.Cartitems
                .FirstOrDefaultAsync(x =>
                    x.Cartid == cartId &&
                    x.Menuitemid == menuItemId);
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
        }

        public async Task AddCartItemAsync(Cartitem item)
        {
            await _context.Cartitems.AddAsync(item);
        }

        public Task UpdateCartItemAsync(Cartitem item)
        {
            _context.Cartitems.Update(item);
            return Task.CompletedTask;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
