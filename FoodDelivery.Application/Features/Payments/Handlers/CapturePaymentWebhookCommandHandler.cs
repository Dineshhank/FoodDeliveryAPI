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
    public class CapturePaymentWebhookCommandHandler
: IRequestHandler<CapturePaymentWebhookCommand, bool>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;

        public CapturePaymentWebhookCommandHandler(
            IPaymentRepository paymentRepository,
            IOrderRepository orderRepository)
        {
            _paymentRepository = paymentRepository;
            _orderRepository = orderRepository;
        }

        public async Task<bool> Handle(
            CapturePaymentWebhookCommand request,
            CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository
                .GetPaymentByProviderOrderIdAsync(request.RazorpayOrderId);

            if (payment == null)
                return false;

            payment.Providerpaymentid = request.RazorpayPaymentId;
            payment.Status = "SUCCESS";

            var order = await _orderRepository.GetOrderByIdAsync(payment.Orderid);

            order.Paymentstatus = "PAID";
            order.Status = "CONFIRMED";

            await _paymentRepository.SaveChangesAsync();

            return true;
        }
    }
}
