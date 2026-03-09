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
    public class OrderRepository : IOrderRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public OrderRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task AddOrderItemsAsync(List<Orderitem> items)
        {
            await _context.Orderitems.AddRangeAsync(items);
        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _context.Orders
                .Include(x => x.Orderitems)
                .FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
