using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Payments.Commands
{
    public class CreateRazorpayOrderCommand : IRequest<string>
    {
        public Guid OrderId { get; set; }

        public decimal Amount { get; set; }

        public string OrderNumber { get; set; }
    }
}
