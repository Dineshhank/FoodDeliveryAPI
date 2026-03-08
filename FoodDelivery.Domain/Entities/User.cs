using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string Fullname { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string Passwordhash { get; set; } = null!;

    public bool Isactive { get; set; }

    public bool Isphoneverified { get; set; }

    public bool Isemailverified { get; set; }

    public DateTime? Lastloginat { get; set; }

    public DateTime Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public DateTime? Deletedat { get; set; }

    public bool Isdeleted { get; set; }

    public DateOnly? Dateofbirth { get; set; }

    public bool? Isprofilecompleted { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Deliverypartner> Deliverypartners { get; set; } = new List<Deliverypartner>();

    public virtual ICollection<Order> OrderDeliverypartners { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderUsers { get; set; } = new List<Order>();

    public virtual ICollection<Orderstatushistory> Orderstatushistories { get; set; } = new List<Orderstatushistory>();

    public virtual ICollection<Refreshtoken> Refreshtokens { get; set; } = new List<Refreshtoken>();

    public virtual ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

    public virtual ICollection<Review> ReviewDeliverypartners { get; set; } = new List<Review>();

    public virtual ICollection<Review> ReviewUsers { get; set; } = new List<Review>();

    public virtual ICollection<Userrole> Userroles { get; set; } = new List<Userrole>();
}
