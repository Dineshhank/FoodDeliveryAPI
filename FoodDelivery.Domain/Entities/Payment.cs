using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Payment
{
    public Guid Id { get; set; }

    public Guid Orderid { get; set; }

    public string Paymentprovider { get; set; } = null!;

    public string Providerpaymentid { get; set; }

    public string? Providerorderid { get; set; }

    public decimal Amount { get; set; }

    public string Status { get; set; } = null!;

    public string? Paymentresponse { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public virtual Order Order { get; set; } = null!;
}
