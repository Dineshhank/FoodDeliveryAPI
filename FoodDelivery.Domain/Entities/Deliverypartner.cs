using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Deliverypartner
{
    public Guid Id { get; set; }

    public Guid Userid { get; set; }

    public Guid Serviceareaid { get; set; }

    public string? Vehicletype { get; set; }

    public string? Vehiclenumber { get; set; }

    public bool? Isavailable { get; set; }

    public decimal? Currentlatitude { get; set; }

    public decimal? Currentlongitude { get; set; }

    public DateTime? Lastactiveat { get; set; }

    public decimal? Avgrating { get; set; }

    public int? Totaldeliveries { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Servicearea Servicearea { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
