using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Restaurant.Commands;
using FoodDelivery.Application.Features.Restaurant.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminMenuController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminMenuController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory(
            CreateCategoryRequest request)
        {
            var id = await _mediator.Send(
                new CreateCategoryCommand { Category = request });

            return Ok(new ApiResponse<Guid>(
                200,
                "Category created successfully",
                id));
        }

        [HttpPost("CreateMenuItem")]
        public async Task<IActionResult> CreateMenuItem(
            CreateMenuItemRequest request)
        {
            var id = await _mediator.Send(
                new CreateMenuItemCommand { MenuItem = request });

            return Ok(new ApiResponse<Guid>(
                200,
                "Menu item created successfully",
                id));
        }
    }
}
