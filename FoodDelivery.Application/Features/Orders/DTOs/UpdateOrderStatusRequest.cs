using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.DTOs
{
    public class UpdateOrderStatusRequest
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
    }
}
