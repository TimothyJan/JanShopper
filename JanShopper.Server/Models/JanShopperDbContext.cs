using Microsoft.EntityFrameworkCore;
using JanShopper.Server.Models;

namespace JanShopper.Server
{
    public class JanShopperDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }

        public JanShopperDbContext(DbContextOptions<JanShopperDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.UserName).IsUnique();
            });

            // Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasIndex(c => c.Name).IsUnique(); // Ensure category names are unique
            });

            // Product entity
            modelBuilder.Entity<Product>(entity =>
            {
                // Configure the relationship between Product and Category
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId);
                entity.Property(p => p.Price)
                    .HasPrecision(18, 2);
            });

            // Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(o => o.TotalAmount)
                      .HasPrecision(18, 2); // Set precision to 18 and scale to 2
            });

            // OrderItems entity
            modelBuilder.Entity<OrderItems>(entity =>
            {
                // Configure the relationship between OrderItems and Order
                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade); // Cascade delete if Order is deleted

                // Configure the relationship between OrderItems and Product
                entity.HasOne(oi => oi.Product)
                      .WithMany(p => p.OrderItems)
                      .HasForeignKey(oi => oi.ProductId)
                      .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if Product is referenced

                // Configure Price precision
                entity.Property(oi => oi.Price)
                      .HasPrecision(18, 2);
            });
        }
    }
}