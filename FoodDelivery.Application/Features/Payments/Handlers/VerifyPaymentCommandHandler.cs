using FoodDelivery.Application.Features.Payments.Commands;
using FoodDelivery.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Payments.Handlers
{
    public class VerifyPaymentCommandHandler
: IRequestHandler<VerifyPaymentCommand, bool>
    {
        private readonly IPaymentGatewayService _gateway;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;

        public VerifyPaymentCommandHandler(
            IPaymentGatewayService gateway,
            IPaymentRepository paymentRepository,
            IOrderRepository orderRepository)
        {
            _gateway = gateway;
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(VerifyPaymentCommand request, CancellationToken cancellationToken)
        {
            var valid = _gateway.VerifyPayment(
                request.RazorpayOrderId,
                request.RazorpayPaymentId,
                request.RazorpaySignature
            );

            if (!valid)
                return false;

            var payment = await _paymentRepository.GetPaymentByOrderIdAsync(request.OrderId);

            payment.Providerpaymentid = request.RazorpayPaymentId;
            payment.Status = "SUCCESS";

            var order = await _orderRepository.GetOrderByIdAsync(request.OrderId);

            order.Paymentstatus = "PAID";
            order.Status = "CONFIRMED";

            await _paymentRepository.SaveChangesAsync();

            return true;
        }
    }
}
