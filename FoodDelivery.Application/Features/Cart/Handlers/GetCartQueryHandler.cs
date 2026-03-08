using FoodDelivery.Application.Features.Cart.Dtos;
using FoodDelivery.Application.Features.Cart.Queries;
using FoodDelivery.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Cart.Handlers
{
    public class GetCartQueryHandler : IRequestHandler<GetCartQuery, CartDto>
    {
        private readonly ICartRepository _cartRepository;

        public GetCartQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId);

            if (cart == null)
                return null;

            return new CartDto
            {
                CartId = cart.Id,
                TotalPrice = cart.Cartitems.Sum(x => x.Price * x.Quantity),
                Items = cart.Cartitems.Select(x => new CartItemDto
                {
                    CartItemId = x.Id,
                    MenuItemId = x.Menuitemid,
                    Quantity = x.Quantity,
                    Price = x.Price
                }).ToList()
            };
        }
    }
}
