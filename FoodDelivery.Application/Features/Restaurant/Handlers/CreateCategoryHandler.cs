using FoodDelivery.Application.Features.Restaurant.Commands;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Handlers
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
{
        private readonly ICategoryRepository _repository;

        public CreateCategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(
            CreateCategoryCommand request,
            CancellationToken cancellationToken)
        {
            var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

            var category = new Category
            {
                Id = Guid.NewGuid(),
                Restaurantid = request.Category.RestaurantId,
                Name = request.Category.Name,
                Displayorder = request.Category.DisplayOrder,
                Createdat = now
            };

            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();

            return category.Id;
        }
    
}
}
