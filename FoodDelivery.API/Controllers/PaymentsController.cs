using FoodDelivery.Application.Features.Payments.Commands;
using FoodDelivery.Application.Features.Payments.Dtos;
using FoodDelivery.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPaymentGatewayService _gateway;

        public PaymentsController(
            IMediator mediator,
            IPaymentGatewayService gateway)
        {
            _mediator = mediator;
            _gateway = gateway;
        }

        [HttpPost("CreateRazorpayOrder")]
        public async Task<IActionResult> CreateOrder(CreateRazorpayOrderRequest request)
        {
            var result = await _mediator.Send(new CreateRazorpayOrderCommand
            {
                OrderId = request.OrderId,
                Amount = request.Amount,
                OrderNumber = request.OrderNumber
            });

            return Ok(result);
        }

        [HttpPost("Verify")]
        public async Task<IActionResult> VerifyPayment(VerifyPaymentRequest request)
        {
            var result = await _mediator.Send(new VerifyPaymentCommand
            {
                OrderId = request.OrderId,
                RazorpayOrderId = request.RazorpayOrderId,
                RazorpayPaymentId = request.RazorpayPaymentId,
                RazorpaySignature = request.RazorpaySignature
            });

            return Ok(result);
        }
        [HttpPost("razorpay-webhook")]
        [AllowAnonymous]
        public async Task<IActionResult> Webhook()
        {
            var body = await new StreamReader(Request.Body).ReadToEndAsync();

            var signature = Request.Headers["X-Razorpay-Signature"].FirstOrDefault();
            if (string.IsNullOrEmpty(signature))
                return Unauthorized();

            var valid = _gateway.VerifyWebhook(body, signature);

            if (!valid)
                return Unauthorized();

            dynamic? payload = JsonConvert.DeserializeObject(body);
            if (payload == null)
                return BadRequest("Invalid payload");

            string? eventType = payload.@event;
            if (eventType == "payment.captured")
            {
                string paymentId = payload.payload.payment.entity.id;
                string orderId = payload.payload.payment.entity.order_id;

                await _mediator.Send(new CapturePaymentWebhookCommand
                {
                    RazorpayOrderId = orderId,
                    RazorpayPaymentId = paymentId
                });
            }

            return Ok();
        }

       
}
}
