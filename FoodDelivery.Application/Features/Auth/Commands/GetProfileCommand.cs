using FoodDelivery.Application.Features.Auth.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Auth.Commands
{
    public class GetProfileCommand: IRequest<GetProfileResponse>
    {
        public Guid UserId { get; set; }
    }
}
