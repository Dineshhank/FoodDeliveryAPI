using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Servicearea
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string State { get; set; } = null!;

    public string Country { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public int Radiusinkm { get; set; }

    public bool Isactive { get; set; }

    public string? Timezone { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public DateTime? Deletedat { get; set; }

    public bool Isdeleted { get; set; }

    public virtual ICollection<Deliverypartner> Deliverypartners { get; set; } = new List<Deliverypartner>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}
