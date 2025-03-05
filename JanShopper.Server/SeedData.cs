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
                var johnDoe = new User
                {
                    UserName = "john_doe",
                    Email = "john@example.com",
                    Password = "Password123!" // Note: Hash the password in a real application
                };
                var janeDoe = new User
                {
                    UserName = "jane_doe",
                    Email = "jane@example.com",
                    Password = "Password123!" // Note: Hash the password in a real application
                };
                var adminUser = new User
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    Password = "Admin123!" // Note: Hash the password in a real application
                };

                context.Users.AddRange(johnDoe, janeDoe, adminUser);
                await context.SaveChangesAsync(); // Save users to generate IDs

                // Add dummy categories
                var electronicsCategory = new Category { Name = "ELECTRONICS" };
                var fashionCategory = new Category { Name = "FASHION" };
                var foodCategory = new Category { Name = "FOOD" };

                context.Categories.AddRange(electronicsCategory, fashionCategory, foodCategory);
                await context.SaveChangesAsync(); // Save categories to generate IDs

                // Add dummy products
                var laptop = new Product
                {
                    Name = "Laptop",
                    Description = "High-performance laptop with 16GB RAM and 512GB SSD",
                    Price = 1200.00m,
                    Stock = 10,
                    CategoryId = electronicsCategory.Id
                };
                var smartphone = new Product
                {
                    Name = "Smartphone",
                    Description = "Latest smartphone with 128GB storage and 5G support",
                    Price = 800.00m,
                    Stock = 25,
                    CategoryId = electronicsCategory.Id
                };
                var headphones = new Product
                {
                    Name = "Headphones",
                    Description = "Noise-cancelling over-ear headphones",
                    Price = 250.00m,
                    Stock = 30,
                    CategoryId = electronicsCategory.Id
                };
                var tShirt = new Product
                {
                    Name = "T-Shirt",
                    Description = "Cotton T-Shirt in various colors",
                    Price = 20.00m,
                    Stock = 50,
                    CategoryId = fashionCategory.Id
                };
                var jeans = new Product
                {
                    Name = "Jeans",
                    Description = "Slim-fit jeans for men",
                    Price = 50.00m,
                    Stock = 40,
                    CategoryId = fashionCategory.Id
                };
                var sneakers = new Product
                {
                    Name = "Sneakers",
                    Description = "Comfortable running shoes",
                    Price = 80.00m,
                    Stock = 20,
                    CategoryId = fashionCategory.Id
                };
                var chocolateBar = new Product
                {
                    Name = "Chocolate Bar",
                    Description = "Dark chocolate bar with 70% cocoa",
                    Price = 5.00m,
                    Stock = 100,
                    CategoryId = foodCategory.Id
                };
                var energyDrink = new Product
                {
                    Name = "Energy Drink",
                    Description = "Sugar-free energy drink",
                    Price = 3.00m,
                    Stock = 75,
                    CategoryId = foodCategory.Id
                };
                var granolaBars = new Product
                {
                    Name = "Granola Bars",
                    Description = "Healthy granola bars with nuts and honey",
                    Price = 4.00m,
                    Stock = 60,
                    CategoryId = foodCategory.Id
                };

                context.Products.AddRange(laptop, smartphone, headphones, tShirt, jeans, sneakers, chocolateBar, energyDrink, granolaBars);
                await context.SaveChangesAsync(); // Save products to generate IDs

                // Add dummy orders
                context.Orders.AddRange(
                    new Order
                    {
                        UserId = johnDoe.Id,
                        OrderDate = DateTime.UtcNow,
                        TotalAmount = 1200.00m, // Laptop
                        Status = "Completed"
                    },
                    new Order
                    {
                        UserId = janeDoe.Id,
                        OrderDate = DateTime.UtcNow,
                        TotalAmount = 800.00m, // Smartphone
                        Status = "Pending"
                    },
                    new Order
                    {
                        UserId = adminUser.Id,
                        OrderDate = DateTime.UtcNow,
                        TotalAmount = 250.00m, // Headphones
                        Status = "Completed"
                    },
                    new Order
                    {
                        UserId = johnDoe.Id,
                        OrderDate = DateTime.UtcNow,
                        TotalAmount = 20.00m, // T-Shirt
                        Status = "Shipped"
                    },
                    new Order
                    {
                        UserId = janeDoe.Id,
                        OrderDate = DateTime.UtcNow,
                        TotalAmount = 50.00m, // Jeans
                        Status = "Pending"
                    },
                    new Order
                    {
                        UserId = adminUser.Id,
                        OrderDate = DateTime.UtcNow,
                        TotalAmount = 80.00m, // Sneakers
                        Status = "Completed"
                    },
                    new Order
                    {
                        UserId = johnDoe.Id,
                        OrderDate = DateTime.UtcNow,
                        TotalAmount = 5.00m, // Chocolate Bar
                        Status = "Shipped"
                    },
                    new Order
                    {
                        UserId = janeDoe.Id,
                        OrderDate = DateTime.UtcNow,
                        TotalAmount = 3.00m, // Energy Drink
                        Status = "Pending"
                    },
                    new Order
                    {
                        UserId = adminUser.Id,
                        OrderDate = DateTime.UtcNow,
                        TotalAmount = 4.00m, // Granola Bars
                        Status = "Completed"
                    }
                );

                await context.SaveChangesAsync(); // Save orders
            }
        }
    }
}