using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Dtos
{
    public class NearbyRestaurantResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string City { get; set; } = null!;

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public bool IsOpen { get; set; }

        public double DistanceKm { get; set; }

        public decimal Rating { get; set; }

        public int DeliveryTimeMinutes { get; set; }

        public int DeliveryFee { get; set; }
    }
}
