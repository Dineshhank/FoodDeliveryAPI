using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Auth.Commands;
using FoodDelivery.Application.Features.Auth.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("RegisterAdmin")]
        [AllowAnonymous] // ⚠ Later restrict this
        public async Task<IActionResult> RegisterAdmin(
    [FromBody] AdminRegisterRequest request)
        {
            var result = await _mediator.Send(new RegisterAdminCommand
            {
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password
            });

            if (!result.Success)
            {
                return BadRequest(new ApiResponse<object>(
                    400,
                    result.Message,
                    null
                ));
            }

            return Ok(new ApiResponse<AdminRegisterResponse>(
                200,
                result.Message,
                result
            ));
        }

        [HttpPost("AdminLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> AdminLogin([FromBody] AdminLoginRequest request)
        {
            var result = await _mediator.Send(new AdminLoginCommand
            {
                Email = request.Email,
                Password = request.Password
            });

            if (!result.Success)
            {
                return BadRequest(new ApiResponse<object>(
                    400,
                    result.Message,
                    null
                ));
            }

            return Ok(new ApiResponse<AdminLoginResponse>(
                200,
                result.Message,
                result
            ));
        }

        [HttpPost("CustomerLogin")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            var result = await _mediator.Send(new LoginCommand { Phone = request.Phone });
            if (!result.Success)
            {
                return BadRequest(new ApiResponse<object>(
                    400,
                    result.Message,
                    null
                ));
            }

            return Ok(new ApiResponse<LoginResponse>(
                200,
                result.Message,
                result
            ));
        }

        [HttpPost("CustomerVerifyOtp")]
        [AllowAnonymous]
        public async Task<ActionResult<VerifyOtpResult>> VerifyOtp([FromBody] VerifyOtpRequest request)
        {
            var result = await _mediator.Send(new VerifyOtpCommand
            {
                Phone = request.Phone,
                Otp = request.Otp
            });

            if (!result.Success)
            {
                return BadRequest(new ApiResponse<object>(
                    400,
                    result.Message,
                    null
                ));
            }

            return Ok(new ApiResponse<VerifyOtpResult>(
                200,
                result.Message,
                result
            ));
        }

        [HttpGet("GetUserProfileInfo")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized(new ApiResponse<object>(
                    401,
                    "Invalid token",
                    null
                ));
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var result = await _mediator.Send(new GetProfileCommand
            {
                UserId = userId
            });

            return Ok(new ApiResponse<GetProfileResponse>(
                200,
                "Profile fetched successfully",
                result
            ));
        }

        [HttpPut("UpdateProfile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(
    [FromBody] UpdateProfileRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized(new ApiResponse<object>(
                    401,
                    "Invalid token",
                    null
                ));
            }

            var userId = Guid.Parse(userIdClaim.Value);

            await _mediator.Send(new UpdateProfileCommand
            {
                UserId = userId,
                Fullname = request.Fullname,
                Dateofbirth = request.Dateofbirth
            });

            return Ok(new ApiResponse<object>(
                200,
                "Profile updated successfully",
                null
            ));
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(
    [FromBody] RefreshTokenRequest request)
        {
            var result = await _mediator.Send(
                new RefreshTokenCommand
                {
                    RefreshToken = request.RefreshToken
                });

            return Ok(new ApiResponse<RefreshTokenResponse>(
                200,
                "Token refreshed successfully",
                result
            ));
        }

        [HttpPost("Logout")]
        [Authorize]
        public async Task<IActionResult> Logout(
    [FromBody] LogoutRequest request)
        {
            await _mediator.Send(
                new LogoutCommand
                {
                    RefreshToken = request.RefreshToken
                });

            return Ok(new ApiResponse<object>(
                200,
                "Logged out successfully",
                null
            ));
        }
    }
}
