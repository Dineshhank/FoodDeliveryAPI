using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Orders.Commands;
using FoodDelivery.Application.Features.Orders.DTOs;
using FoodDelivery.Application.Features.Orders.Queries;
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

        /// <summary>
        /// Single call for the customer “current order” screen: full summary, payments, status, and rider if assigned.
        /// Returns null data when there is no in-progress order (app can then show home).
        /// </summary>
        [HttpGet("GetMyActiveOrder")]
        public async Task<IActionResult> GetMyActiveOrder()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _mediator.Send(new GetMyActiveOrderQuery { UserId = userId });

            var message = result == null
                ? "No active order"
                : "Active order loaded";

            return Ok(new ApiResponse<CustomerActiveOrderDetailDto?>(200, message, result));
        }

        // ✅ Get My Orders
        [HttpGet("GetMyOrders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _mediator.Send(new GetMyOrdersQuery
            {
                UserId = userId
            });

            return Ok(new ApiResponse<List<OrderDto>>(200, "Orders fetched", result));
        }

        // ✅ Get Order By Id
        [HttpGet("GetOrderById")]
        public async Task<IActionResult> GetOrderById(Guid orderId)
        {
            var result = await _mediator.Send(new GetOrderByIdQuery
            {
                OrderId = orderId
            });

            return Ok(new ApiResponse<OrderDto>(200, "Order fetched", result));
        }

        // ✅ Update Status
        [HttpPut("UpdateStatus")]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateOrderStatusRequest request)
        {
            var result = await _mediator.Send(new UpdateOrderStatusCommand
            {
                OrderId = request.OrderId,
                Status = request.Status
            });

            return Ok(new ApiResponse<bool>(200, "Order status updated", result));
        }
    }
}
