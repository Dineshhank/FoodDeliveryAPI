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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly FoodDeliveryDbContext _context;

        public PaymentRepository(FoodDeliveryDbContext context)
        {
            _context = context;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
        }

        public async Task<Payment?> GetPaymentByOrderIdAsync(Guid orderId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(x => x.Orderid == orderId);
        }

        public async Task<Payment?> GetPaymentByProviderOrderIdAsync(string providerOrderId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(x => x.Providerorderid == providerOrderId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
