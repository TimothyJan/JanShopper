using Microsoft.EntityFrameworkCore;
using JanShopper.Server.Models;

namespace JanShopper.Server
{
    public class JanShopperDbContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        public JanShopperDbContext(DbContextOptions<JanShopperDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.UserName)
                        .IsRequired()
                        .HasMaxLength(50);
                entity.Property(u => u.Email)
                        .IsRequired()
                        .HasMaxLength(100);
                entity.Property(u => u.UserName)
                        .IsRequired()
                        .HasMaxLength(50);

                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.UserName).IsUnique();
            });

            // Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
