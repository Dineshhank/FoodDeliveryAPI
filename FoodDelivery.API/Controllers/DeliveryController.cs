using System.Collections.Generic;
using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Delivery.Commands;
using FoodDelivery.Application.Features.Delivery.DTOs;
using FoodDelivery.Application.Features.Delivery.Queries;
using FoodDelivery.Application.Features.Orders.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/delivery")]
    [Authorize(Roles = "DeliveryPartner")]
    public class DeliveryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetUserId() =>
            Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        [HttpPost("toggle-availability")]
        public async Task<IActionResult> ToggleAvailability([FromBody] ToggleAvailabilityRequest request)
        {
            var result = await _mediator.Send(new ToggleAvailabilityCommand
            {
                UserId = GetUserId(),
                IsAvailable = request.IsAvailable
            });

            if (!result)
                return BadRequest(new ApiResponse<object>(400, "Delivery partner profile not found", null));

            return Ok(new ApiResponse<bool>(200, "Availability updated", true));
        }

        [HttpPost("update-location")]
        public async Task<IActionResult> UpdateLocation([FromBody] UpdateLocationRequest request)
        {
            var result = await _mediator.Send(new UpdateLocationCommand
            {
                UserId = GetUserId(),
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                ActiveOrderId = request.ActiveOrderId
            });

            if (!result)
                return BadRequest(new ApiResponse<object>(400, "Delivery partner profile not found", null));

            return Ok(new ApiResponse<bool>(200, "Location updated", true));
        }

        [HttpGet("available-orders")]
        public async Task<IActionResult> GetAvailableOrders()
        {
            var list = await _mediator.Send(new GetAvailableDeliveryOrdersQuery
            {
                UserId = GetUserId()
            });

            return Ok(new ApiResponse<List<AvailableDeliveryOrderDto>>(
                200,
                "Available orders fetched",
                list));
        }

        [HttpGet("my-orders")]
        public async Task<IActionResult> GetMyOrders()
        {
            var list = await _mediator.Send(new GetMyDeliveryOrdersQuery
            {
                UserId = GetUserId()
            });

            return Ok(new ApiResponse<List<OrderDto>>(200, "Orders fetched", list));
        }

        [HttpPost("reached-restaurant")]
        public async Task<IActionResult> ReachedRestaurant([FromBody] ReachedRestaurantRequest request)
        {
            var result = await _mediator.Send(new ReachedRestaurantCommand
            {
                UserId = GetUserId(),
                OrderId = request.OrderId
            });

            if (!result)
                return BadRequest(new ApiResponse<object>(400, "Unable to confirm arrival (check GPS near restaurant)", null));

            return Ok(new ApiResponse<bool>(200, "Arrival at restaurant recorded", true));
        }

        [HttpPost("start-to-customer")]
        public async Task<IActionResult> StartToCustomer([FromBody] StartDeliveryToCustomerRequest request)
        {
            var result = await _mediator.Send(new StartDeliveryToCustomerCommand
            {
                UserId = GetUserId(),
                OrderId = request.OrderId
            });

            if (!result)
                return BadRequest(new ApiResponse<object>(400, "Unable to start leg to customer", null));

            return Ok(new ApiResponse<bool>(200, "En route to customer", true));
        }

        [HttpPost("accept-order")]
        public async Task<IActionResult> AcceptOrder([FromBody] AcceptOrderRequest request)
        {
            var result = await _mediator.Send(new AcceptOrderCommand
            {
                UserId = GetUserId(),
                OrderId = request.OrderId
            });

            if (!result)
                return BadRequest(new ApiResponse<object>(400, "Unable to accept order", null));

            return Ok(new ApiResponse<bool>(200, "Order accepted", true));
        }

        [HttpPost("pickup")]
        public async Task<IActionResult> Pickup([FromBody] PickupOrderRequest request)
        {
            var result = await _mediator.Send(new PickupOrderCommand
            {
                UserId = GetUserId(),
                OrderId = request.OrderId
            });

            if (!result)
                return BadRequest(new ApiResponse<object>(400, "Pickup failed — need ReachedRestaurant and GPS within ~150m of restaurant", null));

            return Ok(new ApiResponse<bool>(200, "Pickup recorded", true));
        }

        [HttpPost("complete")]
        public async Task<IActionResult> Complete([FromBody] CompleteDeliveryRequest request)
        {
            var result = await _mediator.Send(new CompleteDeliveryCommand
            {
                UserId = GetUserId(),
                OrderId = request.OrderId
            });

            if (!result)
                return BadRequest(new ApiResponse<object>(400, "Complete failed — need OnTheWayToCustomer and GPS within ~150m of customer", null));

            return Ok(new ApiResponse<bool>(200, "Delivery completed", true));
        }
    }
}
