using FoodDelivery.Application.Features.Cart.Commands;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Cart.Handlers
{
    public class AddItemToCartHandler : IRequestHandler<AddItemToCartCommand, Guid>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMenuItemRepository _menuItemRepository;

        public AddItemToCartHandler(
            ICartRepository cartRepository,
            IMenuItemRepository menuItemRepository)
        {
            _cartRepository = cartRepository;
            _menuItemRepository = menuItemRepository;
        }

        public async Task<Guid> Handle(
            AddItemToCartCommand request,
            CancellationToken cancellationToken)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(request.MenuItemId);

            if (menuItem == null)
                throw new Exception("Menu item not found");

            var cart = await _cartRepository
                .GetUserCartByRestaurantAsync(request.UserId, menuItem.Restaurantid);

            if (cart == null)
            {
                cart = new Domain.Entities.Cart
                {
                    Id = Guid.NewGuid(),
                    Userid = request.UserId,
                    Restaurantid = menuItem.Restaurantid,
                    Createdat = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
                };

                await _cartRepository.AddCartAsync(cart);
            }

            var cartItem = await _cartRepository
                .GetCartItemAsync(cart.Id, request.MenuItemId);

            if (cartItem != null)
            {
                cartItem.Quantity += request.Quantity;
                cartItem.Totalamount = cartItem.Quantity * cartItem.Price;

                await _cartRepository.UpdateCartItemAsync(cartItem);
            }
            else
            {
                var newItem = new Cartitem
                {
                    Id = Guid.NewGuid(),
                    Cartid = cart.Id,
                    Menuitemid = request.MenuItemId,
                    Quantity = request.Quantity,
                    Price = menuItem.Price,
                    Totalamount = menuItem.Price * request.Quantity,
                    Createdat = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified)
                };

                await _cartRepository.AddCartItemAsync(newItem);
            }

            await _cartRepository.SaveChangesAsync();

            return cart.Id;
        }
    }
}
