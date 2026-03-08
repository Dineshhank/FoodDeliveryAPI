using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Restaurant
{
    public Guid Id { get; set; }

    public Guid? Ownerid { get; set; }

    public Guid Serviceareaid { get; set; }

    public string Name { get; set; } = null!;

    public string Slug { get; set; } = null!;

    public string? Description { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string Address { get; set; } = null!;

    public string? City { get; set; }

    public string? Pincode { get; set; }

    public bool? Isopen { get; set; }

    public bool? Isactive { get; set; }

    public bool? Isverified { get; set; }

    public decimal? Avgrating { get; set; }

    public int? Totalratings { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public DateTime? Deletedat { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Menuitem> Menuitems { get; set; } = new List<Menuitem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual User? Owner { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Servicearea Servicearea { get; set; } = null!;
}
