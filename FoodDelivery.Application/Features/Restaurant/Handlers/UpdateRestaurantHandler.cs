using FoodDelivery.Application.Features.Restaurant.Commands;
using FoodDelivery.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Handlers
{
    public class UpdateRestaurantHandler
     : IRequestHandler<UpdateRestaurantCommand, bool>
    {
        private readonly IRestaurantRepository _repository;

        public UpdateRestaurantHandler(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(
            UpdateRestaurantCommand request,
            CancellationToken cancellationToken)
        {
            var restaurant = await _repository.GetByIdAsync(request.RestaurantId);
            var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);
            if (restaurant == null)
                throw new Exception("Restaurant not found");

            restaurant.Name = request.Restaurant.Name;
            restaurant.Address = request.Restaurant.Address;
            restaurant.City = request.Restaurant.City;
            restaurant.Phone = request.Restaurant.Phone;
            restaurant.Updatedat = now;

            await _repository.UpdateAsync(restaurant);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
