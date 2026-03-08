using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Cartitem
{
    public Guid Id { get; set; }

    public Guid Cartid { get; set; }

    public Guid Menuitemid { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public decimal Totalamount { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Menuitem Menuitem { get; set; } = null!;
}
