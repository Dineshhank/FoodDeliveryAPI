using FoodDelivery.Application.Features.Orders.DTOs;

namespace FoodDelivery.Application.Features.Delivery.DTOs
{
    public class AvailableDeliveryOrderDto
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; } = string.Empty;
        public decimal FinalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string DeliveryAddress { get; set; } = string.Empty;
        public decimal? DeliveryLatitude { get; set; }
        public decimal? DeliveryLongitude { get; set; }
        public Guid RestaurantId { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public decimal RestaurantLatitude { get; set; }
        public decimal RestaurantLongitude { get; set; }
        public string RestaurantAddress { get; set; } = string.Empty;
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
