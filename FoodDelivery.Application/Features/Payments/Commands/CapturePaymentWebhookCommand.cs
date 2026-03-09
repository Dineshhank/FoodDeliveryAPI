using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Payments.Commands
{
    public class CapturePaymentWebhookCommand : IRequest<bool>
    {
        public string RazorpayOrderId { get; set; }

        public string RazorpayPaymentId { get; set; }
    }
}
