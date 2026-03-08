using FoodDelivery.Application.Features.Cart.Commands;
using FoodDelivery.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Cart.Handlers
{
    public class UpdateCartItemQuantityHandler : IRequestHandler<UpdateCartItemQuantityCommand, bool>
    {
        private readonly ICartRepository _cartRepository;

        public UpdateCartItemQuantityHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<bool> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId);

            if (cart == null)
                return false;

            var item = await _cartRepository.GetCartItemAsync(cart.Id, request.MenuItemId);

            if (item == null)
                return false;

            item.Quantity = request.Quantity;

            await _cartRepository.UpdateCartItemAsync(item);

            await _cartRepository.SaveChangesAsync();

            return true;
        }
    }
}
