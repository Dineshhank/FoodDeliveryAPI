using FoodDelivery.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Infrastructure.ExternalServices.Payments
{
    public class RazorpayPaymentService : IPaymentGatewayService
    {
        private readonly IConfiguration _config;

        public RazorpayPaymentService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> CreateOrderAsync(string receipt, decimal amount)
        {
            var key = _config["Razorpay:Key"];
            var secret = _config["Razorpay:Secret"];

            RazorpayClient client = new RazorpayClient(key, secret);

            Dictionary<string, object> options = new();

            options.Add("amount", (int)(amount * 100));
            options.Add("currency", "INR");
            options.Add("receipt", receipt);

            Razorpay.Api.Order order = client.Order.Create(options);

            return order["id"].ToString();
        }

        public bool VerifyPayment(string orderId, string paymentId, string signature)
        {
            var secret = _config["Razorpay:Secret"];

            string payload = orderId + "|" + paymentId;

            var hash = new HMACSHA256(Encoding.UTF8.GetBytes(secret));

            var generatedSignature = BitConverter
                .ToString(hash.ComputeHash(Encoding.UTF8.GetBytes(payload)))
                .Replace("-", "")
                .ToLower();

            return generatedSignature == signature;
        }

        public bool VerifyWebhook(string body, string signature)
        {
            var secret = _config["Razorpay:WebhookSecret"];

            var hash = new HMACSHA256(Encoding.UTF8.GetBytes(secret));

            var generatedSignature = BitConverter
                .ToString(hash.ComputeHash(Encoding.UTF8.GetBytes(body)))
                .Replace("-", "")
                .ToLower();

            return generatedSignature == signature;
        }
    }
}
