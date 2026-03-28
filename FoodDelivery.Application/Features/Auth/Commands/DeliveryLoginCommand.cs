using FoodDelivery.Application.Features.Auth.DTOs;
using MediatR;

namespace FoodDelivery.Application.Features.Auth.Commands
{
    public class DeliveryLoginCommand : IRequest<DeliveryLoginResponse>
    {
        public string Phone { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
