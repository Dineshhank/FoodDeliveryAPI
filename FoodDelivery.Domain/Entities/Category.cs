using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Category
{
    public Guid Id { get; set; }

    public Guid Restaurantid { get; set; }

    public string Name { get; set; } = null!;

    public int? Displayorder { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Menuitem> Menuitems { get; set; } = new List<Menuitem>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
