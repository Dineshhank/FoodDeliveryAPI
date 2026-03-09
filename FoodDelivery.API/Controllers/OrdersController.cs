using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Orders.Commands;
using FoodDelivery.Application.Features.Orders.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateOrderFromCart")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var userId = Guid.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var orderId = await _mediator.Send(new CreateOrderFromCartCommand
            {
                UserId = userId,
                PaymentMethod = request.PaymentMethod,
                DeliveryAddress = request.DeliveryAddress,
                DeliveryLatitude = request.DeliveryLatitude,
                DeliveryLongitude = request.DeliveryLongitude
            });

            return Ok(new ApiResponse<Guid>(
                200,
                "Order created successfully",
                orderId
            ));
        }
    }
}
