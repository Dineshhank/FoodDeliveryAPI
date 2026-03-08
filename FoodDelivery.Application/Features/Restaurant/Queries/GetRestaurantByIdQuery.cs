using FoodDelivery.Application.Features.Restaurant.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Queries
{
    public class GetRestaurantByIdQuery : IRequest<RestaurantResponse>
    
    {
        public Guid Id { get; set; }
    }
}
