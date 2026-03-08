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
    public class CreateMenuItemHandler
     : IRequestHandler<CreateMenuItemCommand, Guid>
    {
        private readonly IMenuItemRepository _repository;

        public CreateMenuItemHandler(IMenuItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(
            CreateMenuItemCommand request,
            CancellationToken cancellationToken)
        {
            var now = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified);

            var item = new Menuitem
            {
                Id = Guid.NewGuid(),
                Restaurantid = request.MenuItem.RestaurantId,
                Categoryid = request.MenuItem.CategoryId,
                Name = request.MenuItem.Name,
                Description = request.MenuItem.Description,
                Price = request.MenuItem.Price,
                Discountprice = request.MenuItem.DiscountPrice,
                Isveg = request.MenuItem.IsVeg,
                Isrecommended = request.MenuItem.IsRecommended,
                Preparationtime = request.MenuItem.PreparationTime,
                Createdat = now,
                Isdeleted = false
            };

            await _repository.AddAsync(item);
            await _repository.SaveChangesAsync();

            return item.Id;
        }
    }
}
