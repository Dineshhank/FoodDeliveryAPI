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
    public class GetRestaurantByIdHandler  : IRequestHandler<GetRestaurantByIdQuery, RestaurantResponse>
    {
        private readonly IRestaurantRepository _repository;

        public GetRestaurantByIdHandler(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task<RestaurantResponse> Handle(
            GetRestaurantByIdQuery request,
            CancellationToken cancellationToken)
        {
            var r = await _repository.GetByIdAsync(request.Id);

            if (r == null)
                throw new Exception("Restaurant not found");

            return new RestaurantResponse
            {
                Id = r.Id,
                Name = r.Name,
                Slug = r.Slug,
                Address = r.Address,
                City = r.City,
                Phone = r.Phone,
                Email = r.Email,
                Latitude = r.Latitude,
                Longitude = r.Longitude,
                IsActive = (bool)r.Isactive,
                IsOpen = (bool)r.Isopen
            };
        }
    }
}
