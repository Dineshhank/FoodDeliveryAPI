using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Cart.Dtos
{
    public class RemoveCartItemCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid MenuItemId { get; set; }
    }
}
