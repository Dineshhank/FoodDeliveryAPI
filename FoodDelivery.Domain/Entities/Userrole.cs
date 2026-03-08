using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Userrole
{
    public Guid Userid { get; set; }

    public Guid Roleid { get; set; }

    public Guid Id { get; set; }

    public DateTime? Createdat { get; set; }

    public bool? Isactive { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
