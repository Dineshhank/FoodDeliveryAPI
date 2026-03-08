using FoodDelivery.Application.Features.Restaurant.Dtos;
using FoodDelivery.Application.Features.Restaurant.Queries;
using FoodDelivery.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Handlers
{
    public class GetNearbyRestaurantsHandler : IRequestHandler<GetNearbyRestaurantsQuery, List<NearbyRestaurantResponse>>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public GetNearbyRestaurantsHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<List<NearbyRestaurantResponse>> Handle(
            GetNearbyRestaurantsQuery request,
            CancellationToken cancellationToken)
        {
            var data = await _restaurantRepository.GetNearbyRestaurantsAsync(
                request.Latitude,
                request.Longitude,
                request.RadiusKm);

            return data.Select(x =>
            {
                var distance = Math.Round(x.Distance, 2);

                // Delivery time calculation
                var deliveryTime = CalculateDeliveryTime(distance);

                // Delivery fee calculation
                var deliveryFee = distance > 5 ? 30 : 0;

                return new NearbyRestaurantResponse
                {
                    Id = x.Restaurant.Id,
                    Name = x.Restaurant.Name,
                    Address = x.Restaurant.Address,
                    City = x.Restaurant.City,
                    Latitude = x.Restaurant.Latitude,
                    Longitude = x.Restaurant.Longitude,
                    IsOpen = x.Restaurant.Isopen ?? false,
                    DistanceKm = distance,
                    Rating = x.Restaurant.Avgrating ?? 0,
                    DeliveryTimeMinutes = deliveryTime,
                    DeliveryFee = deliveryFee
                };
            }).ToList();
        }

        private int CalculateDeliveryTime(double distanceKm)
        {
            // base preparation time
            var baseTime = 15;

            // travel time
            var travelTime = (int)Math.Ceiling(distanceKm * 4);

            return baseTime + travelTime;
        }
    }
}
