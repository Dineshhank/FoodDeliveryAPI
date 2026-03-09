using FoodDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Interfaces
{
    public interface IPaymentRepository
    {
        Task AddPaymentAsync(Payment payment);

        Task<Payment?> GetPaymentByOrderIdAsync(Guid orderId);

        Task<Payment?> GetPaymentByProviderOrderIdAsync(string providerOrderId);

        Task SaveChangesAsync();
    }
}
