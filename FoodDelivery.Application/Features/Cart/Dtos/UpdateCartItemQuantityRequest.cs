using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Cart.Dtos
{
    public class UpdateCartItemQuantityRequest
    {
        public Guid MenuItemId { get; set; }

        public int Quantity { get; set; }
    }
}
