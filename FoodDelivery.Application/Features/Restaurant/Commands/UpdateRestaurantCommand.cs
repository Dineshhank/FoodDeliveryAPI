using FoodDelivery.Application.Features.Restaurant.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Commands
{
    public class UpdateRestaurantCommand : IRequest<bool>
    {
        public Guid RestaurantId { get; set; }
        public UpdateRestaurantRequest Restaurant { get; set; } = null!;
    }
}
