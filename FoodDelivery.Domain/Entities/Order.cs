using System;
using System.Collections.Generic;

namespace FoodDelivery.Domain.Entities;

public partial class Order
{
    public Guid Id { get; set; }

    public string Ordernumber { get; set; } = null!;

    public Guid Userid { get; set; }

    public Guid Restaurantid { get; set; }

    public Guid? Deliverypartnerid { get; set; }

    public Guid Serviceareaid { get; set; }

    public decimal Subtotal { get; set; }

    public decimal? Deliveryfee { get; set; }

    public decimal? Taxamount { get; set; }

    public decimal? Discountamount { get; set; }

    public decimal Finalamount { get; set; }

    public string Paymentmethod { get; set; } = null!;

    public string Paymentstatus { get; set; } = null!;

    public string Status { get; set; } = null!;

    public string Deliveryaddress { get; set; } = null!;

    public decimal? Deliverylatitude { get; set; }

    public decimal? Deliverylongitude { get; set; }

    public DateTime? Estimateddeliverytime { get; set; }

    public DateTime? Deliveredat { get; set; }

    public DateTime? Cancelledat { get; set; }

    public string? Cancellationreason { get; set; }

    public DateTime? Createdat { get; set; }

    public DateTime? Updatedat { get; set; }

    public DateTime? Deletedat { get; set; }

    public bool? Isdeleted { get; set; }

    public virtual User? Deliverypartner { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual ICollection<Orderstatushistory> Orderstatushistories { get; set; } = new List<Orderstatushistory>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Restaurant Restaurant { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Servicearea Servicearea { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
