using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Auth.Commands;
using FoodDelivery.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryAuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DeliveryAuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterDeliveryPartnerRequest request)
        {
            var result = await _mediator.Send(new RegisterDeliveryPartnerCommand
            {
                FullName = request.FullName,
                Phone = request.Phone,
                Password = request.Password,
                VehicleType = request.VehicleType,
                VehicleNumber = request.VehicleNumber
            });

            if (!result.Success || result.UserId == null)
            {
                return BadRequest(new ApiResponse<object>(
                    400,
                    result.Message,
                    null));
            }

            return Ok(new ApiResponse<Guid>(
                200,
                result.Message,
                result.UserId.Value));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] DeliveryLoginRequest request)
        {
            var result = await _mediator.Send(new DeliveryLoginCommand
            {
                Phone = request.Phone,
                Password = request.Password
            });

            if (!result.Success)
            {
                return BadRequest(new ApiResponse<object>(
                    400,
                    result.Message,
                    null));
            }

            return Ok(new ApiResponse<DeliveryLoginResponse>(
                200,
                result.Message,
                result));
        }
    }
}
