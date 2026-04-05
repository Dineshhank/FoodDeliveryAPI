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

        public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _context.Orders
                .Where(x => x.Userid == userId)
                .Include(x => x.Orderitems)
                .ToListAsync();
        }

        public async Task<Order?> GetActiveIncompleteOrderForUserAsync(
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o =>
                    o.Userid == userId &&
                    (o.Isdeleted == null || o.Isdeleted == false) &&
                    o.Cancelledat == null &&
                    o.Status != null &&
                    o.Status.ToLower() != "delivered" &&
                    o.Status.ToLower() != "cancelled")
                .Include(o => o.Orderitems)
                    .ThenInclude(i => i.Menuitem)
                .Include(o => o.Restaurant)
                .Include(o => o.Deliverypartner)
                .Include(o => o.Payments)
                .OrderByDescending(o => o.Createdat)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Order>> GetOrdersByDeliveryPartnerIdAsync(
            Guid deliveryPartnerUserId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Where(x =>
                    x.Deliverypartnerid == deliveryPartnerUserId &&
                    (x.Isdeleted == null || x.Isdeleted == false))
                .Include(x => x.Orderitems)
                .OrderByDescending(x => x.Createdat)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Order>> GetAvailableOrdersForDeliveryAsync(
            Guid serviceAreaId,
            CancellationToken cancellationToken = default)
        {
            return await _context.Orders
                .Where(o =>
                    o.Serviceareaid == serviceAreaId &&
                    o.Deliverypartnerid == null &&
                    o.Status == "CONFIRMED" &&
                    (o.Isdeleted == null || o.Isdeleted == false))
                .Include(o => o.Orderitems)
                .Include(o => o.Restaurant)
                .OrderBy(o => o.Createdat)
                .ToListAsync(cancellationToken);
        }

        public Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            return Task.CompletedTask;
        }

        public async Task AddOrderStatusHistoryAsync(Orderstatushistory history)
        {
            await _context.Orderstatushistories.AddAsync(history);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
