using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodDelivery.Application.Features.Restaurant.Dtos
{
    public class CategoryMenuDto
    {
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public List<MenuItemDto> Items { get; set; } = new();
    }
}
