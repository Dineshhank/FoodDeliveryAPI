using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Dtos
{
    public class MenuItemDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public decimal? DiscountPrice { get; set; }

        public bool IsVeg { get; set; }

        public bool IsRecommended { get; set; }

        public int PreparationTime { get; set; }
    }
}
