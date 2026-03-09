using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Interfaces
{
    public interface IPaymentGatewayService
    {
        Task<string> CreateOrderAsync(string receipt, decimal amount);

        bool VerifyPayment(string razorpayOrderId, string razorpayPaymentId, string signature);

        bool VerifyWebhook(string body, string signature);
    }
}
