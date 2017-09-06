using MatOrderingService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MatOrderingService.Services.Storage
{
    public class OrdersDbContext: DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options): base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MapOrders(modelBuilder.Entity<Order>());
            MapProducts(modelBuilder.Entity<Product>());
        }

        protected void MapOrders(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(p => p.Id)
                .IsRequired();
            builder.Property(p => p.Status)
                .IsRequired()
                .HasDefaultValue(OrderStatus.New);
            builder.Property(p => p.CreatorId)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.IsDeleted)
                .IsRequired();
            builder.Property(p => p.CreateDate)
                .IsRequired()
                .ForSqlServerHasDefaultValueSql("GETDATE()");
            builder.HasIndex(o => o.OrderCode)
                .IsUnique();

            builder.HasMany(p => p.OrderItems)
                .WithOne(o => o.Order)
                .HasForeignKey(i => i.OrderId);

            builder.HasKey(p => p.Id);
        }

        protected void MapProducts(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.Property(p => p.Id)
                .IsRequired();
            builder.Property(p => p.Code)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();
            builder.HasKey(p => p.Id);
        }

        protected void MapOrderItems(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.Property(p => p.Count)
                .IsRequired();

            builder.HasOne(i => i.Product)
                .WithOne()
                .HasForeignKey<OrderItem>(i => i.ProductId);

            builder.HasOne(i => i.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(i => i.OrderId);

            builder.HasKey(p => p.Id);
        }
    }
}