using System;
using System.Collections.Generic;
using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Persistence.Context;

public partial class FoodDeliveryDbContext : DbContext
{
    public FoodDeliveryDbContext()
    {
    }

    public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Cartitem> Cartitems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Deliverypartner> Deliverypartners { get; set; }

    public virtual DbSet<Menuitem> Menuitems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Orderitem> Orderitems { get; set; }

    public virtual DbSet<Orderstatushistory> Orderstatushistories { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Refreshtoken> Refreshtokens { get; set; }

    public virtual DbSet<Restaurant> Restaurants { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Servicearea> Serviceareas { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Userotp> Userotps { get; set; }

    public virtual DbSet<Userrole> Userroles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=fooddelivery_db;Username=postgres;Password=postgres123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cart_pkey");

            entity.ToTable("cart");

            entity.HasIndex(e => e.Restaurantid, "idx_cart_restaurant");

            entity.HasIndex(e => e.Userid, "idx_cart_user");

            entity.HasIndex(e => new { e.Userid, e.Restaurantid }, "uq_user_restaurant_cart").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cart_restaurantid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cart_userid_fkey");
        });

        modelBuilder.Entity<Cartitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cartitems_pkey");

            entity.ToTable("cartitems");

            entity.HasIndex(e => e.Cartid, "idx_cartitems_cart");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Cartid).HasColumnName("cartid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Menuitemid).HasColumnName("menuitemid");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Totalamount)
                .HasPrecision(10, 2)
                .HasColumnName("totalamount");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Cart).WithMany(p => p.Cartitems)
                .HasForeignKey(d => d.Cartid)
                .HasConstraintName("cartitems_cartid_fkey");

            entity.HasOne(d => d.Menuitem).WithMany(p => p.Cartitems)
                .HasForeignKey(d => d.Menuitemid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cartitems_menuitemid_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.HasIndex(e => e.Restaurantid, "idx_category_restaurant");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Displayorder)
                .HasDefaultValue(0)
                .HasColumnName("displayorder");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Categories)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("categories_restaurantid_fkey");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("coupons_pkey");

            entity.ToTable("coupons");

            entity.HasIndex(e => e.Code, "coupons_code_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasColumnName("code");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Discounttype)
                .HasMaxLength(20)
                .HasColumnName("discounttype");
            entity.Property(e => e.Expirydate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expirydate");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Maxdiscount)
                .HasPrecision(10, 2)
                .HasColumnName("maxdiscount");
            entity.Property(e => e.Minorderamount)
                .HasPrecision(10, 2)
                .HasColumnName("minorderamount");
            entity.Property(e => e.Usagelimit).HasColumnName("usagelimit");
            entity.Property(e => e.Usedcount)
                .HasDefaultValue(0)
                .HasColumnName("usedcount");
            entity.Property(e => e.Value)
                .HasPrecision(10, 2)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Deliverypartner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("deliverypartners_pkey");

            entity.ToTable("deliverypartners");

            entity.HasIndex(e => e.Isavailable, "idx_delivery_available");

            entity.HasIndex(e => e.Serviceareaid, "idx_delivery_servicearea");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Avgrating)
                .HasPrecision(3, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgrating");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Currentlatitude)
                .HasPrecision(10, 8)
                .HasColumnName("currentlatitude");
            entity.Property(e => e.Currentlongitude)
                .HasPrecision(11, 8)
                .HasColumnName("currentlongitude");
            entity.Property(e => e.Isavailable)
                .HasDefaultValue(true)
                .HasColumnName("isavailable");
            entity.Property(e => e.Lastactiveat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastactiveat");
            entity.Property(e => e.Serviceareaid).HasColumnName("serviceareaid");
            entity.Property(e => e.Totaldeliveries)
                .HasDefaultValue(0)
                .HasColumnName("totaldeliveries");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Userid).HasColumnName("userid");
            entity.Property(e => e.Vehiclenumber)
                .HasMaxLength(50)
                .HasColumnName("vehiclenumber");
            entity.Property(e => e.Vehicletype)
                .HasMaxLength(50)
                .HasColumnName("vehicletype");

            entity.HasOne(d => d.Servicearea).WithMany(p => p.Deliverypartners)
                .HasForeignKey(d => d.Serviceareaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deliverypartners_serviceareaid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Deliverypartners)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("deliverypartners_userid_fkey");
        });

        modelBuilder.Entity<Menuitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("menuitems_pkey");

            entity.ToTable("menuitems");

            entity.HasIndex(e => e.Isavailable, "idx_menu_available");

            entity.HasIndex(e => e.Restaurantid, "idx_menu_restaurant");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Discountprice)
                .HasPrecision(10, 2)
                .HasColumnName("discountprice");
            entity.Property(e => e.Imageurl).HasColumnName("imageurl");
            entity.Property(e => e.Isavailable)
                .HasDefaultValue(true)
                .HasColumnName("isavailable");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Isrecommended)
                .HasDefaultValue(false)
                .HasColumnName("isrecommended");
            entity.Property(e => e.Isveg)
                .HasDefaultValue(false)
                .HasColumnName("isveg");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Preparationtime).HasColumnName("preparationtime");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Category).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.Categoryid)
                .HasConstraintName("menuitems_categoryid_fkey");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Menuitems)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("menuitems_restaurantid_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pkey");

            entity.ToTable("orders");

            entity.HasIndex(e => e.Serviceareaid, "idx_orders_servicearea");

            entity.HasIndex(e => e.Status, "idx_orders_status");

            entity.HasIndex(e => e.Userid, "idx_orders_user");

            entity.HasIndex(e => e.Ordernumber, "orders_ordernumber_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Cancellationreason).HasColumnName("cancellationreason");
            entity.Property(e => e.Cancelledat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("cancelledat");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Deliveredat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deliveredat");
            entity.Property(e => e.Deliveryaddress).HasColumnName("deliveryaddress");
            entity.Property(e => e.Deliveryfee)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("deliveryfee");
            entity.Property(e => e.Deliverylatitude)
                .HasPrecision(10, 8)
                .HasColumnName("deliverylatitude");
            entity.Property(e => e.Deliverylongitude)
                .HasPrecision(11, 8)
                .HasColumnName("deliverylongitude");
            entity.Property(e => e.Deliverypartnerid).HasColumnName("deliverypartnerid");
            entity.Property(e => e.Discountamount)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("discountamount");
            entity.Property(e => e.Estimateddeliverytime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("estimateddeliverytime");
            entity.Property(e => e.Finalamount)
                .HasPrecision(10, 2)
                .HasColumnName("finalamount");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Ordernumber)
                .HasMaxLength(50)
                .HasColumnName("ordernumber");
            entity.Property(e => e.Paymentmethod)
                .HasMaxLength(50)
                .HasColumnName("paymentmethod");
            entity.Property(e => e.Paymentstatus)
                .HasMaxLength(50)
                .HasColumnName("paymentstatus");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Serviceareaid).HasColumnName("serviceareaid");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Subtotal)
                .HasPrecision(10, 2)
                .HasColumnName("subtotal");
            entity.Property(e => e.Taxamount)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("taxamount");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Deliverypartner).WithMany(p => p.OrderDeliverypartners)
                .HasForeignKey(d => d.Deliverypartnerid)
                .HasConstraintName("orders_deliverypartnerid_fkey");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Restaurantid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_restaurantid_fkey");

            entity.HasOne(d => d.Servicearea).WithMany(p => p.Orders)
                .HasForeignKey(d => d.Serviceareaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_serviceareaid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OrderUsers)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_userid_fkey");
        });

        modelBuilder.Entity<Orderitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderitems_pkey");

            entity.ToTable("orderitems");

            entity.HasIndex(e => e.Orderid, "idx_orderitems_order");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Menuitemid).HasColumnName("menuitemid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Totalamount)
                .HasPrecision(10, 2)
                .HasColumnName("totalamount");

            entity.HasOne(d => d.Menuitem).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.Menuitemid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderitems_menuitemid_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderitems)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("orderitems_orderid_fkey");
        });

        modelBuilder.Entity<Orderstatushistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orderstatushistory_pkey");

            entity.ToTable("orderstatushistory");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Changedat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("changedat");
            entity.Property(e => e.Changedby).HasColumnName("changedby");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.ChangedbyNavigation).WithMany(p => p.Orderstatushistories)
                .HasForeignKey(d => d.Changedby)
                .HasConstraintName("orderstatushistory_changedby_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Orderstatushistories)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orderstatushistory_orderid_fkey");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payments_pkey");

            entity.ToTable("payments");

            entity.HasIndex(e => e.Orderid, "idx_payments_order");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Paymentprovider)
                .HasMaxLength(50)
                .HasColumnName("paymentprovider");
            entity.Property(e => e.Paymentresponse)
                .HasColumnType("jsonb")
                .HasColumnName("paymentresponse");
            entity.Property(e => e.Providerorderid)
                .HasMaxLength(150)
                .HasColumnName("providerorderid");
            entity.Property(e => e.Providerpaymentid)
                .HasMaxLength(150)
                .HasColumnName("providerpaymentid");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Order).WithMany(p => p.Payments)
                .HasForeignKey(d => d.Orderid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payments_orderid_fkey");
        });

        modelBuilder.Entity<Refreshtoken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("refreshtokens_pkey");

            entity.ToTable("refreshtokens");

            entity.HasIndex(e => e.Token, "idx_refreshtoken_token");

            entity.HasIndex(e => e.Userid, "idx_refreshtoken_userid");

            entity.HasIndex(e => e.Token, "uq_refreshtoken").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Expiresat).HasColumnName("expiresat");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(100)
                .HasColumnName("ipaddress");
            entity.Property(e => e.Isrevoked)
                .HasDefaultValue(false)
                .HasColumnName("isrevoked");
            entity.Property(e => e.Isused)
                .HasDefaultValue(false)
                .HasColumnName("isused");
            entity.Property(e => e.Jwtid).HasColumnName("jwtid");
            entity.Property(e => e.Revokedat).HasColumnName("revokedat");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.Refreshtokens)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("refreshtokens_userid_fkey");
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("restaurants_pkey");

            entity.ToTable("restaurants");

            entity.HasIndex(e => e.Isactive, "idx_restaurant_active");

            entity.HasIndex(e => new { e.Latitude, e.Longitude }, "idx_restaurant_location");

            entity.HasIndex(e => e.Serviceareaid, "idx_restaurant_servicearea");

            entity.HasIndex(e => e.Slug, "restaurants_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Address).HasColumnName("address");
            entity.Property(e => e.Avgrating)
                .HasPrecision(3, 2)
                .HasDefaultValueSql("0")
                .HasColumnName("avgrating");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Isopen)
                .HasDefaultValue(true)
                .HasColumnName("isopen");
            entity.Property(e => e.Isverified)
                .HasDefaultValue(false)
                .HasColumnName("isverified");
            entity.Property(e => e.Latitude)
                .HasPrecision(10, 8)
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasPrecision(11, 8)
                .HasColumnName("longitude");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Ownerid).HasColumnName("ownerid");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.Pincode)
                .HasMaxLength(10)
                .HasColumnName("pincode");
            entity.Property(e => e.Serviceareaid).HasColumnName("serviceareaid");
            entity.Property(e => e.Slug)
                .HasMaxLength(200)
                .HasColumnName("slug");
            entity.Property(e => e.Totalratings)
                .HasDefaultValue(0)
                .HasColumnName("totalratings");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");

            entity.HasOne(d => d.Owner).WithMany(p => p.Restaurants)
                .HasForeignKey(d => d.Ownerid)
                .HasConstraintName("restaurants_ownerid_fkey");

            entity.HasOne(d => d.Servicearea).WithMany(p => p.Restaurants)
                .HasForeignKey(d => d.Serviceareaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("restaurants_serviceareaid_fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reviews_pkey");

            entity.ToTable("reviews");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deliverypartnerid).HasColumnName("deliverypartnerid");
            entity.Property(e => e.Orderid).HasColumnName("orderid");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Restaurantid).HasColumnName("restaurantid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Deliverypartner).WithMany(p => p.ReviewDeliverypartners)
                .HasForeignKey(d => d.Deliverypartnerid)
                .HasConstraintName("reviews_deliverypartnerid_fkey");

            entity.HasOne(d => d.Order).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Orderid)
                .HasConstraintName("reviews_orderid_fkey");

            entity.HasOne(d => d.Restaurant).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.Restaurantid)
                .HasConstraintName("reviews_restaurantid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.ReviewUsers)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("reviews_userid_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Name, "roles_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Servicearea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("serviceareas_pkey");

            entity.ToTable("serviceareas");

            entity.HasIndex(e => e.Isactive, "idx_servicearea_active");

            entity.HasIndex(e => e.Slug, "idx_servicearea_slug");

            entity.HasIndex(e => e.Slug, "serviceareas_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Country)
                .HasMaxLength(150)
                .HasColumnName("country");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Deletedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Latitude)
                .HasPrecision(10, 8)
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasPrecision(11, 8)
                .HasColumnName("longitude");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Radiusinkm)
                .HasDefaultValue(15)
                .HasColumnName("radiusinkm");
            entity.Property(e => e.Slug)
                .HasMaxLength(150)
                .HasColumnName("slug");
            entity.Property(e => e.State)
                .HasMaxLength(150)
                .HasColumnName("state");
            entity.Property(e => e.Timezone)
                .HasMaxLength(100)
                .HasDefaultValueSql("'Asia/Kolkata'::character varying")
                .HasColumnName("timezone");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Isactive, "idx_users_active");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Phone, "users_phone_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Dateofbirth).HasColumnName("dateofbirth");
            entity.Property(e => e.Deletedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deletedat");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(150)
                .HasColumnName("fullname");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Isdeleted)
                .HasDefaultValue(false)
                .HasColumnName("isdeleted");
            entity.Property(e => e.Isemailverified)
                .HasDefaultValue(false)
                .HasColumnName("isemailverified");
            entity.Property(e => e.Isphoneverified)
                .HasDefaultValue(false)
                .HasColumnName("isphoneverified");
            entity.Property(e => e.Isprofilecompleted)
                .HasDefaultValue(false)
                .HasColumnName("isprofilecompleted");
            entity.Property(e => e.Lastloginat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastloginat");
            entity.Property(e => e.Passwordhash).HasColumnName("passwordhash");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.Updatedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updatedat");
        });

        modelBuilder.Entity<Userotp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("userotps_pkey");

            entity.ToTable("userotps");

            entity.HasIndex(e => e.Phone, "idx_userotps_phone");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Attemptcount)
                .HasDefaultValue(0)
                .HasColumnName("attemptcount");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Expiresat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("expiresat");
            entity.Property(e => e.Isused)
                .HasDefaultValue(false)
                .HasColumnName("isused");
            entity.Property(e => e.Otpcode)
                .HasMaxLength(6)
                .HasColumnName("otpcode");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Userrole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("userroles_pkey");

            entity.ToTable("userroles");

            entity.HasIndex(e => new { e.Userid, e.Roleid }, "uq_user_role").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnName("createdat");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.Role).WithMany(p => p.Userroles)
                .HasForeignKey(d => d.Roleid)
                .HasConstraintName("userroles_roleid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Userroles)
                .HasForeignKey(d => d.Userid)
                .HasConstraintName("userroles_userid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
