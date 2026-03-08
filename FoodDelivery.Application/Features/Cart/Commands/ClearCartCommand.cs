using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Cart.Commands
{
    public class ClearCartCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
    }
}
