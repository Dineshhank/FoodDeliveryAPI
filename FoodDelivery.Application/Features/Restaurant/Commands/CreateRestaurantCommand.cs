using FoodDelivery.Application.Features.Restaurant.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Commands
{
    public class CreateRestaurantCommand : IRequest<Guid>
    {
        public CreateRestaurantRequest Restaurant { get; set; } = null!;
    }
}
