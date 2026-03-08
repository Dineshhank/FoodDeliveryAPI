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
    public class GetRestaurantMenuHandler
     : IRequestHandler<GetRestaurantMenuQuery, RestaurantMenuResponse>
    {
        private readonly IRestaurantRepository _repository;

        public GetRestaurantMenuHandler(IRestaurantRepository repository)
        {
            _repository = repository;
        }

        public async Task<RestaurantMenuResponse> Handle(
            GetRestaurantMenuQuery request,
            CancellationToken cancellationToken)
        {
            var categories = await _repository
                .GetCategoriesByRestaurantAsync(request.RestaurantId);

            var items = await _repository
                .GetMenuItemsByRestaurantAsync(request.RestaurantId);

            var result = new RestaurantMenuResponse
            {
                RestaurantId = request.RestaurantId,
                Categories = categories.Select(c => new CategoryMenuDto
                {
                    CategoryId = c.Id,
                    CategoryName = c.Name,
                    Items = items
                        .Where(i => i.Categoryid == c.Id)
                        .Select(i => new MenuItemDto
                        {
                            Id = i.Id,
                            Name = i.Name,
                            Description = i.Description,
                            Price = i.Price,
                            DiscountPrice = i.Discountprice,
                            IsVeg = i.Isveg ?? false,
                            IsRecommended = i.Isrecommended ?? false,
                            PreparationTime = i.Preparationtime ?? 0
                        }).ToList()
                }).ToList()
            };

            return result;
        }
    }
}
