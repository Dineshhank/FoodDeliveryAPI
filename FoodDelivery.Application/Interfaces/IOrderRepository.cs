using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);

        Task AddOrderItemsAsync(List<Orderitem> items);

        Task<Order?> GetOrderByIdAsync(Guid orderId);

        Task<List<Order>> GetOrdersByUserIdAsync(Guid userId);

        /// <summary>
        /// Latest order for the customer that is not delivered, not cancelled, and not soft-deleted.
        /// </summary>
        Task<Order?> GetActiveIncompleteOrderForUserAsync(Guid userId, CancellationToken cancellationToken = default);
        /// <param name="deliveryPartnerUserId">users.id of the rider (orders.deliverypartnerid).</param>
        Task<List<Order>> GetOrdersByDeliveryPartnerIdAsync(Guid deliveryPartnerUserId, CancellationToken cancellationToken = default);

        /// <summary>
        /// Paid, confirmed orders in a service area with no rider assigned yet (same rules as accept-order).
        /// </summary>
        Task<List<Order>> GetAvailableOrdersForDeliveryAsync(Guid serviceAreaId, CancellationToken cancellationToken = default);

        Task UpdateOrderAsync(Order order);
        Task AddOrderStatusHistoryAsync(Orderstatushistory history);
        Task SaveChangesAsync();
    }
}
