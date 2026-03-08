using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Dtos
{
    public class RestaurantResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string City { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public string? Email { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public bool IsActive { get; set; }
        public bool IsOpen { get; set; }
    }
}
