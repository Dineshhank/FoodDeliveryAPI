using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.DTOs
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; }
        public decimal FinalAmount { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<OrderItemDto> Items { get; set; }
    }
}
