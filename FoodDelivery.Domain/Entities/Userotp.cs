using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Userotp
{
    public Guid Id { get; set; }

    public string Phone { get; set; } = null!;

    public string Otpcode { get; set; } = null!;

    public bool? Isused { get; set; }

    public int? Attemptcount { get; set; }

    public DateTime Expiresat { get; set; }

    public DateTime? Createdat { get; set; }
}
