using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Restaurant.Commands;
using FoodDelivery.Application.Features.Restaurant.Dtos;
using FoodDelivery.Application.Features.Restaurant.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminRestaurantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminRestaurantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateRestaurant")]
        public async Task<IActionResult> Create(
            CreateRestaurantRequest request)
        {
            var id = await _mediator.Send(
                new CreateRestaurantCommand
                {
                    Restaurant = request
                });

            return Ok(new ApiResponse<Guid>(
                200,
                "Restaurant created",
                id));
        }

        [HttpGet("GetAllRestaurants")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _mediator.Send(new GetRestaurantsQuery());

            return Ok(new ApiResponse<List<RestaurantResponse>>(
                200,
                "Restaurants fetched",
                data));
        }

        [HttpGet("GetRestaurantById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _mediator.Send(
                new GetRestaurantByIdQuery { Id = id });

            return Ok(new ApiResponse<RestaurantResponse>(
                200,
                "Restaurant fetched",
                data));
        }

        [HttpPut("UpdateRestaurant")]
        public async Task<IActionResult> Update(
            Guid id,
            UpdateRestaurantRequest request)
        {
            await _mediator.Send(
                new UpdateRestaurantCommand
                {
                    RestaurantId = id,
                    Restaurant = request
                });

            return Ok(new ApiResponse<object>(
                200,
                "Restaurant updated",
                null));
        }
    }
}
