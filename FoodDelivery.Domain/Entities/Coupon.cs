using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Coupon
{
    public Guid Id { get; set; }

    public string Code { get; set; } = null!;

    public string? Discounttype { get; set; }

    public decimal? Value { get; set; }

    public decimal? Maxdiscount { get; set; }

    public decimal? Minorderamount { get; set; }

    public int? Usagelimit { get; set; }

    public int? Usedcount { get; set; }

    public DateTime? Expirydate { get; set; }

    public bool? Isactive { get; set; }

    public DateTime? Createdat { get; set; }
}
