using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.DTOs
{
    public class OrderItemDto
    {
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
