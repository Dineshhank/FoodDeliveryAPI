using FoodDelivery.Application.Features.Restaurant.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Queries
{
    public class GetNearbyRestaurantsQuery:IRequest<List<NearbyRestaurantResponse>>
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int RadiusKm { get; set; } = 20;
    }
}
