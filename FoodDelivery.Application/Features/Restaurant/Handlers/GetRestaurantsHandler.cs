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
    public class GetRestaurantsHandler  : IRequestHandler<GetRestaurantsQuery, List<RestaurantResponse>>
    {
    private readonly IRestaurantRepository _repository;

    public GetRestaurantsHandler(IRestaurantRepository repository)
    {
        _repository = repository;
    }

        public async Task<List<RestaurantResponse>> Handle(
            GetRestaurantsQuery request,
            CancellationToken cancellationToken)
        {
            var restaurants = await _repository.GetAllAsync();

            return restaurants.Select(x => new RestaurantResponse
            {
                Id = x.Id,
                Name = x.Name,
                Slug = x.Slug,
                Address = x.Address,
                City = x.City,
                Phone = x.Phone,
                Email = x.Email,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                IsActive = (bool)x.Isactive,
                IsOpen = (bool)x.Isopen
            }).ToList();
        }
}
}
