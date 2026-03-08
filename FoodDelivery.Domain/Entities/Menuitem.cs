using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Menuitem
{
    public Guid Id { get; set; }

    public Guid Restaurantid { get; set; }

    public Guid? Categoryid { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public decimal? Discountprice { get; set; }

    public bool? Isveg { get; set; }

    public bool? Isavailable { get; set; }

    public bool? Isrecommended { get; set; }

    public string? Imageurl { get; set; }

    public int? Preparationtime { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public DateTime? Deletedat { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Restaurant Restaurant { get; set; } = null!;
}
