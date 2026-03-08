using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Cart
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public Guid Restaurantid { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
