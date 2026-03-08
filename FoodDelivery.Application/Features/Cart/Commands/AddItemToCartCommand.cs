using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Cart.Commands
{
    public class AddItemToCartCommand :IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public Guid MenuItemId { get; set; }

        public int Quantity { get; set; }
    }
}
