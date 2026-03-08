using FoodDelivery.Application.Common.Models;
using FoodDelivery.Application.Features.Cart.Commands;
using FoodDelivery.Application.Features.Cart.Dtos;
using FoodDelivery.Application.Features.Cart.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodDelivery.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] AddItemToCartRequest request)
        {
            var userId = Guid.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var cartId = await _mediator.Send(new AddItemToCartCommand
            {
                UserId = userId,
                MenuItemId = request.MenuItemId,
                Quantity = request.Quantity
            });

            return Ok(new ApiResponse<Guid>(
                200,
                "Item added to cart successfully",
                cartId
            ));
        }

        [HttpGet("GetCartByUserId")]
        public async Task<IActionResult> GetCart()
        {
            var userId = Guid.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var cart = await _mediator.Send(new GetCartQuery
            {
                UserId = userId
            });

            return Ok(new ApiResponse<CartDto>(
                200,
                "Cart fetched successfully",
                cart
            ));
        }

        [HttpPut("UpdateQuantity")]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateCartItemQuantityRequest request)
        {
            var userId = Guid.Parse(
               User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _mediator.Send(new UpdateCartItemQuantityCommand
            {
                UserId = userId,
                MenuItemId = request.MenuItemId,
                Quantity = request.Quantity
            });

            return Ok(new ApiResponse<bool>(
                200,
                "Cart item quantity updated",
                result
            ));
        }

        // REMOVE ITEM
        [HttpDelete("RemoveItem/{menuItemId}")]
        public async Task<IActionResult> RemoveItem(Guid menuItemId)
        {
            var userId = Guid.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _mediator.Send(new RemoveCartItemCommand
            {
                UserId = userId,
                MenuItemId = menuItemId
            });

            return Ok(new ApiResponse<bool>(
                200,
                "Item removed from cart",
                result
            ));
        }

        // CLEAR CART
        [HttpDelete("Clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = Guid.Parse(
               User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var result = await _mediator.Send(new ClearCartCommand
            {
                UserId = userId
            });

            return Ok(new ApiResponse<bool>(
                200,
                "Cart cleared successfully",
                result
            ));
        }
    }
}
