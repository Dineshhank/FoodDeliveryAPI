using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Payments.Dtos
{
    public class VerifyPaymentRequest
    {
        public Guid OrderId { get; set; }

        public string RazorpayOrderId { get; set; }

        public string RazorpayPaymentId { get; set; }

        public string RazorpaySignature { get; set; }
    }
}
