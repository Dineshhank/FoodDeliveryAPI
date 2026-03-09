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


        Task SaveChangesAsync();
    }
}
