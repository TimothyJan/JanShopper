using JanShopper.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace JanShopper.Server.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new JanShopperDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<JanShopperDbContext>>()))
            {
                // Check if the database already has data
                if (context.Users.Any())
                {
                    return; // Database has been seeded
                }

                // Add dummy users
                context.Users.AddRange(
                    new User
                    {
                        UserName = "john_doe",
                        Email = "john@example.com",
                        Password = "Password123!" // Note: Hash the password in a real application
                    },
                    new User
                    {
                        UserName = "jane_doe",
                        Email = "jane@example.com",
                        Password = "Password123!" // Note: Hash the password in a real application
                    },
                    new User
                    {
                        UserName = "admin",
                        Email = "admin@example.com",
                        Password = "Admin123!" // Note: Hash the password in a real application
                    }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}