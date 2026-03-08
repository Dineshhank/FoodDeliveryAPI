using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Auth.Commands
{
    public class LogoutCommand :IRequest<bool>
    {
        public string RefreshToken { get; set; } = null!;
    }
}
