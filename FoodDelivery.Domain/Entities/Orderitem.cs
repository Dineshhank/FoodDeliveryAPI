using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Orderitem
{
    public Guid Id { get; set; }

    public Guid Orderid { get; set; }

    public Guid Menuitemid { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Totalamount { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual Menuitem Menuitem { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
