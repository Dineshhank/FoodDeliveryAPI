using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Restaurant.Dtos;
using FoodDelivery.Application.Features.Restaurant.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RestaurantsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearbyRestaurants(
    [FromQuery] double lat,
    [FromQuery] double lng,
    [FromQuery] int radius = 20)
        {
            var result = await _mediator.Send(
                new GetNearbyRestaurantsQuery
                {
                    Latitude = lat,
                    Longitude = lng,
                    RadiusKm = radius
                });

            return Ok(new ApiResponse<List<NearbyRestaurantResponse>>(
                200,
                "Nearby restaurants fetched successfully",
                result));
        }

        [HttpGet("GetRestaurantsById")]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _mediator.Send(
                new GetRestaurantByIdQuery { Id = id });

            return Ok(new ApiResponse<RestaurantResponse>(
                200,
                "Restaurant fetched",
                data));
        }

        [HttpGet("GetRestaurantMenusByRestaurantid")]
        public async Task<IActionResult> GetRestaurantMenu(Guid restaurantId)
        {
            var result = await _mediator.Send(
                new GetRestaurantMenuQuery
                {
                    RestaurantId = restaurantId
                });

            return Ok(new ApiResponse<RestaurantMenuResponse>(
                200,
                "Restaurant menu fetched successfully",
                result));
        }
    }
}
