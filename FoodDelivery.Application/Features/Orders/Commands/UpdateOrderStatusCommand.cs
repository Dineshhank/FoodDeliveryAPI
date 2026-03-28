using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.Commands
{
    public class UpdateOrderStatusCommand : IRequest<bool>
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
    }
}
