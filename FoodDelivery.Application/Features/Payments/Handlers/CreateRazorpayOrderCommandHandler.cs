using FoodDelivery.Application.Features.Payments.Commands;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Payments.Handlers
{
    public class CreateRazorpayOrderCommandHandler
: IRequestHandler<CreateRazorpayOrderCommand, string>
    {
        private readonly IPaymentGatewayService _gateway;
        private readonly IPaymentRepository _paymentRepository;

        public CreateRazorpayOrderCommandHandler(
            IPaymentGatewayService gateway,
            IPaymentRepository paymentRepository)
        {
            _gateway = gateway;
            _paymentRepository = paymentRepository;
        }

        public async Task<string> Handle(CreateRazorpayOrderCommand request, CancellationToken cancellationToken)
        {
            var razorpayOrderId = await _gateway.CreateOrderAsync(
                request.OrderNumber,
                request.Amount
            );

            var payment = new Payment
            {
                Id = Guid.NewGuid(),
                Orderid = request.OrderId,
                Paymentprovider = "Razorpay",
                Providerorderid = razorpayOrderId,
                Amount = request.Amount,
                Status = "PENDING",
                Createdat = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
            };

            await _paymentRepository.AddPaymentAsync(payment);
            await _paymentRepository.SaveChangesAsync();

            return razorpayOrderId;
        }
    }
}
