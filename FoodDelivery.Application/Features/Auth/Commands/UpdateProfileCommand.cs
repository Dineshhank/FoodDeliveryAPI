using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Auth.Commands
{
    public class UpdateProfileCommand: IRequest<bool>
    {
        public Guid UserId { get; set; }
        public string? Fullname { get; set; }
        public DateOnly? Dateofbirth { get; set; }
    }
}
