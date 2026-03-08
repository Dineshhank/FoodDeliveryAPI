using FoodDelivery.Application.Features.Restaurant.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Queries
{
    public class GetRestaurantMenuQuery: IRequest<RestaurantMenuResponse>
    {
        public Guid RestaurantId { get; set; }
    }
}
