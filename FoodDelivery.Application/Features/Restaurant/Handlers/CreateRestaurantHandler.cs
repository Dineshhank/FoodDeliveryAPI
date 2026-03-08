using FoodDelivery.Application.Features.Restaurant.Commands;
using FoodDelivery.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDelivery.Domain.Entities;


namespace FoodDelivery.Application.Features.Restaurant.Handlers
{
    public class CreateRestaurantHandler : IRequestHandler<CreateRestaurantCommand, Guid>
    {
        private readonly IRestaurantRepository _repository;

        public CreateRestaurantHandler(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(
            CreateRestaurantCommand request,
            CancellationToken cancellationToken)
        {
            var r = request.Restaurant;
            var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            var restaurant = new Domain.Entities.Restaurant
            {
                Id = Guid.NewGuid(),
                Name = r.Name,
                Slug = r.Slug,
                Address = r.Address,
                City = r.City,
                Phone = r.Phone,
                Email = r.Email,
                Latitude = r.Latitude,
                Longitude = r.Longitude,
                Serviceareaid = r.ServiceAreaId,
                Ownerid = null,
                Isactive = true,
                Isopen = true,
                Createdat = now
            };

            await _repository.AddAsync(restaurant);
            await _repository.SaveChangesAsync();

            return restaurant.Id;
        }
    }
}
