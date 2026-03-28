using FoodDelivery.Application.Common.Geo;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Features.Delivery
{
    internal static class DeliveryLocationRules
    {
        internal static bool HasDriverPosition(Deliverypartner partner) =>
            partner.Currentlatitude != null && partner.Currentlongitude != null;

        internal static bool DriverNearRestaurant(Deliverypartner partner, Order order, double maxMeters)
        {
            if (!HasDriverPosition(partner))
                return false;
            if (order.Restaurantlatitude == null || order.Restaurantlongitude == null)
                return true;
            return GeoDistance.MetersBetween(
                       partner.Currentlatitude!.Value,
                       partner.Currentlongitude!.Value,
                       order.Restaurantlatitude.Value,
                       order.Restaurantlongitude.Value)
                   <= maxMeters;
        }

        internal static bool DriverNearCustomer(Deliverypartner partner, Order order, double maxMeters)
        {
            if (!HasDriverPosition(partner))
                return false;
            if (order.Deliverylatitude == null || order.Deliverylongitude == null)
                return true;
            return GeoDistance.MetersBetween(
                       partner.Currentlatitude!.Value,
                       partner.Currentlongitude!.Value,
                       order.Deliverylatitude.Value,
                       order.Deliverylongitude.Value)
                   <= maxMeters;
        }
    }
}
