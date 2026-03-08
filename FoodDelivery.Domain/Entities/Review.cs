using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Review
{
    public Guid Id { get; set; }

    public Guid? Orderid { get; set; }

    public Guid? Userid { get; set; }

    public Guid? Restaurantid { get; set; }

    public Guid? Deliverypartnerid { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual User? Deliverypartner { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Restaurant? Restaurant { get; set; }

    public virtual User? User { get; set; }
}
