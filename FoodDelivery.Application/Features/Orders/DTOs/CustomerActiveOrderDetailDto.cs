using System;
using System.Collections.Generic;

namespace FoodDelivery.Application.Features.Orders.DTOs
{
    /// <summary>
    /// Full customer-facing snapshot for the in-progress order (home / “my order” screen).
    /// </summary>
    public class CustomerActiveOrderDetailDto
    {
        public Guid OrderId { get; set; }
        public string OrderNumber { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string PaymentStatus { get; set; } = null!;
        public string PaymentMethod { get; set; } = null!;

        public decimal Subtotal { get; set; }
        public decimal? DeliveryFee { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }

        public string DeliveryAddress { get; set; } = null!;
        public decimal? DeliveryLatitude { get; set; }
        public decimal? DeliveryLongitude { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? EstimatedDeliveryTime { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public RestaurantOrderSummaryDto? Restaurant { get; set; }
        public List<CustomerOrderLineItemDto> Items { get; set; } = new();
        public List<OrderPaymentSummaryDto> Payments { get; set; } = new();
        public AssignedDeliveryPersonDto? DeliveryPerson { get; set; }
    }

    public class CustomerOrderLineItemDto
    {
        public Guid OrderItemId { get; set; }
        public Guid MenuItemId { get; set; }
        public string Name { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
    }

    public class RestaurantOrderSummaryDto
    {
        public Guid RestaurantId { get; set; }
        public string Name { get; set; } = null!;
        public string? Slug { get; set; }
        public string Address { get; set; } = null!;
        public string? City { get; set; }
        public string? Phone { get; set; }
        public decimal? OrderSnapshotLatitude { get; set; }
        public decimal? OrderSnapshotLongitude { get; set; }
    }

    public class OrderPaymentSummaryDto
    {
        public Guid PaymentId { get; set; }
        public string Provider { get; set; } = null!;
        public string Status { get; set; } = null!;
        public decimal Amount { get; set; }
        public string? ProviderOrderId { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class AssignedDeliveryPersonDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string? Phone { get; set; }
        public string? VehicleType { get; set; }
        public string? VehicleNumber { get; set; }
        public decimal? CurrentLatitude { get; set; }
        public decimal? CurrentLongitude { get; set; }
    }
}
