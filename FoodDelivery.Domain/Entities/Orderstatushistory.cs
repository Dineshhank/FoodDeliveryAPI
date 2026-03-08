using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Orderstatushistory
{
    public Guid Id { get; set; }

    public Guid Orderid { get; set; }

    public string Status { get; set; } = null!;

    public Guid? Changedby { get; set; }

    public DateTime? Changedat { get; set; }

    public virtual User? ChangedbyNavigation { get; set; }

    public virtual Order Order { get; set; } = null!;
}
