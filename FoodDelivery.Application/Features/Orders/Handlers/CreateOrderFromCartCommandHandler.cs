using FoodDelivery.Application.Features.Orders.Commands;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.Handlers
{
    public class CreateOrderFromCartCommandHandler : IRequestHandler<CreateOrderFromCartCommand, Guid>
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderFromCartCommandHandler(
            ICartRepository cartRepository,
            IOrderRepository orderRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
        }

        public async Task<Guid> Handle(CreateOrderFromCartCommand request, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(request.UserId);

            if (cart == null || !cart.Cartitems.Any())
                throw new Exception("Cart is empty");

            var subTotal = cart.Cartitems.Sum(x => x.Price * x.Quantity);

            decimal deliveryFee = 40;
            decimal tax = subTotal * 0.05m;
            decimal discount = 0;

            var finalAmount = subTotal + deliveryFee + tax - discount;

            var order = new Order
            {
                Id = Guid.NewGuid(),
                Ordernumber = $"ORD-{DateTime.UtcNow.Ticks}",

                Userid = request.UserId,
                Restaurantid = cart.Restaurantid,
                Serviceareaid = Guid.Parse("460c6de4-dba8-487e-8bc8-a3db6c907ff3"), // replace with actual

                Subtotal = subTotal,
                Deliveryfee = deliveryFee,
                Taxamount = tax,
                Discountamount = discount,
                Finalamount = finalAmount,

                Paymentmethod = request.PaymentMethod,
                Paymentstatus = "Pending",
                Status = "Created",

                Deliveryaddress = request.DeliveryAddress,
                Deliverylatitude = request.DeliveryLatitude,
                Deliverylongitude = request.DeliveryLongitude,

                Createdat = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Unspecified),
            };

            await _orderRepository.AddOrderAsync(order);

            var orderItems = cart.Cartitems.Select(x => new Orderitem
            {
                Id = Guid.NewGuid(),
                Orderid = order.Id,
                Menuitemid = x.Menuitemid,
                Quantity = x.Quantity,
                Price = x.Price,
                Totalamount = x.Price * x.Quantity
            }).ToList();

            await _orderRepository.AddOrderItemsAsync(orderItems);

            await _orderRepository.SaveChangesAsync();

            await _cartRepository.ClearCartAsync(cart.Id);
            await _cartRepository.SaveChangesAsync();

            return order.Id;
        }
    }
}
