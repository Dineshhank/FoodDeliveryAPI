using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Dtos
{
    public class CreateCategoryRequest
    {
        public Guid RestaurantId { get; set; }

        public string Name { get; set; } = null!;

        public int DisplayOrder { get; set; }
    }
}
