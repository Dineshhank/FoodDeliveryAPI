using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.Commands
{
    public class CreateOrderFromCartCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }

        public string PaymentMethod { get; set; }

        public string DeliveryAddress { get; set; }

        public decimal DeliveryLatitude { get; set; }

        public decimal DeliveryLongitude { get; set; }
    }
}
