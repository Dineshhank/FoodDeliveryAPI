using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Dtos
{
    public class RestaurantMenuResponse
    {
        public Guid RestaurantId { get; set; }

        public List<CategoryMenuDto> Categories { get; set; } = new();
    }
}
