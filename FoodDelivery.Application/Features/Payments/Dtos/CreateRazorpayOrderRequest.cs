using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Payments.Dtos
{
    public class CreateRazorpayOrderRequest
    {
        public Guid OrderId { get; set; }

        public decimal Amount { get; set; }

        public string OrderNumber { get; set; }
    }
}
