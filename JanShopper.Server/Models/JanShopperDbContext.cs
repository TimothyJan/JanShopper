using Microsoft.EntityFrameworkCore;
using JanShopper.Server.Models;

namespace JanShopper.Server
{
    public class JanShopperDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

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
        }
    }
}