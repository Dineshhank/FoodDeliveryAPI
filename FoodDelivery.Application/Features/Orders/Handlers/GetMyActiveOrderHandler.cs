using FoodDelivery.Application.Features.Orders.DTOs;
using FoodDelivery.Application.Features.Orders.Queries;
using FoodDelivery.Application.Interfaces;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Orders.Handlers
{
    public class GetMyActiveOrderHandler : IRequestHandler<GetMyActiveOrderQuery, CustomerActiveOrderDetailDto?>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public GetMyActiveOrderHandler(
            IOrderRepository orderRepository,
            IDeliveryRepository deliveryRepository)
        {
            _orderRepository = orderRepository;
            _deliveryRepository = deliveryRepository;
        }

        public async Task<CustomerActiveOrderDetailDto?> Handle(
            GetMyActiveOrderQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetActiveIncompleteOrderForUserAsync(
                request.UserId,
                cancellationToken);

            if (order == null)
                return null;

            AssignedDeliveryPersonDto? deliveryDto = null;
            if (order.Deliverypartnerid is { } riderUserId)
            {
                var partner = await _deliveryRepository.GetByUserIdAsync(riderUserId, cancellationToken);
                var riderUser = partner?.User ?? order.Deliverypartner;
                if (riderUser != null)
                {
                    deliveryDto = new AssignedDeliveryPersonDto
                    {
                        UserId = riderUser.Id,
                        FullName = riderUser.Fullname,
                        Phone = riderUser.Phone,
                        VehicleType = partner?.Vehicletype,
                        VehicleNumber = partner?.Vehiclenumber,
                        CurrentLatitude = partner?.Currentlatitude,
                        CurrentLongitude = partner?.Currentlongitude
                    };
                }
            }

            RestaurantOrderSummaryDto? restaurantDto = null;
            if (order.Restaurant != null)
            {
                restaurantDto = new RestaurantOrderSummaryDto
                {
                    RestaurantId = order.Restaurant.Id,
                    Name = order.Restaurant.Name,
                    Slug = order.Restaurant.Slug,
                    Address = order.Restaurant.Address,
                    City = order.Restaurant.City,
                    Phone = order.Restaurant.Phone,
                    OrderSnapshotLatitude = order.Restaurantlatitude,
                    OrderSnapshotLongitude = order.Restaurantlongitude
                };
            }

            return new CustomerActiveOrderDetailDto
            {
                OrderId = order.Id,
                OrderNumber = order.Ordernumber,
                Status = order.Status,
                PaymentStatus = order.Paymentstatus,
                PaymentMethod = order.Paymentmethod,
                Subtotal = order.Subtotal,
                DeliveryFee = order.Deliveryfee,
                TaxAmount = order.Taxamount,
                DiscountAmount = order.Discountamount,
                FinalAmount = order.Finalamount,
                DeliveryAddress = order.Deliveryaddress,
                DeliveryLatitude = order.Deliverylatitude,
                DeliveryLongitude = order.Deliverylongitude,
                CreatedAt = order.Createdat,
                EstimatedDeliveryTime = order.Estimateddeliverytime,
                DeliveredAt = order.Deliveredat,
                UpdatedAt = order.Updatedat,
                Restaurant = restaurantDto,
                Items = order.Orderitems
                    .Select(i => new CustomerOrderLineItemDto
                    {
                        OrderItemId = i.Id,
                        MenuItemId = i.Menuitemid,
                        Name = i.Menuitem?.Name ?? "Item",
                        ImageUrl = i.Menuitem?.Imageurl,
                        Quantity = i.Quantity,
                        UnitPrice = i.Price,
                        LineTotal = i.Totalamount
                    })
                    .ToList(),
                Payments = order.Payments
                    .OrderByDescending(p => p.Createdat)
                    .Select(p => new OrderPaymentSummaryDto
                    {
                        PaymentId = p.Id,
                        Provider = p.Paymentprovider,
                        Status = p.Status,
                        Amount = p.Amount,
                        ProviderOrderId = p.Providerorderid,
                        CreatedAt = p.Createdat
                    })
                    .ToList(),
                DeliveryPerson = deliveryDto
            };
        }
    }
}
