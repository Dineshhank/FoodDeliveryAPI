using FoodDelivery.Application.Features.Auth.DTOs;
using MediatR;

namespace FoodDelivery.Application.Features.Auth.Commands
{
    public class VerifyOtpCommand : IRequest<VerifyOtpResult>
    {
        public string Phone { get; set; } = string.Empty;
        public string Otp { get; set; } = string.Empty;
    }
}
