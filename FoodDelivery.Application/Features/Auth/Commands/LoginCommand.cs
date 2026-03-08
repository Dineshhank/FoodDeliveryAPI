using FoodDelivery.Application.Features.Auth.DTOs;
using MediatR;

namespace FoodDelivery.Application.Features.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Phone { get; set; }
    }
}
