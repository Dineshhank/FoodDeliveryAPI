using FoodDelivery.Application.Features.Auth.DTOs;
using MediatR;

namespace FoodDelivery.Application.Features.Auth.Commands
{
    public class RegisterDeliveryPartnerCommand : IRequest<RegisterDeliveryPartnerResult>
    {
        public string FullName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? VehicleType { get; set; }
        public string? VehicleNumber { get; set; }
    }
}
