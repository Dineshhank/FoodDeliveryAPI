using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Refreshtoken
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public string Token { get; set; } = null!;

    public string Jwtid { get; set; } = null!;

    public bool Isrevoked { get; set; }

    public bool Isused { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime Expiresat { get; set; }

    public DateTime? Revokedat { get; set; }

    public string? Ipaddress { get; set; }

    public virtual User User { get; set; } = null!;
}
