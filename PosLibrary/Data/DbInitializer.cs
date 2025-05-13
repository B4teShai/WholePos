using Microsoft.EntityFrameworkCore;
using PosLibrary.Models;
using System;
using System.Data;
using System.Data.Common;

namespace PosLibrary.Data
{
    /// <summary>
    /// Өгөгдлийн сангид жишээ өгөгдөл байгаа эсэхийг шалгана.
    /// </summary>
    public static class DbInitializer
    {
        /// <summary>
        /// Өгөгдлийн сангид жишээ ангиллын жагсаалт болон бүтээгдэхүүнүүд байгаа эсэхийг шалгана.
        /// </summary>
        /// <param name="context">Өгөгдлийн сангийн контекст.</param>
        /// <returns>Асинхрон операцийн тайлбар.</returns>
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!await context.Users.AnyAsync())
            {
                await context.Users.AddRangeAsync(new List<User>
                {
                    new Manager { Username = "manager", Password = "manager123", IsActive = true },
                    new Cashier { Username = "cashier1", Password = "cashier123", IsActive = true },
                    new Cashier { Username = "cashier2", Password = "cashier123", IsActive = true }
                });
                await context.SaveChangesAsync();
            }

            // Өгөгдлийн сангид өгөгдөл байгаа эсэхийг шалгана
            if (await context.Categories.AnyAsync())
            {
                return; 
            }

            // Жишээ ангиллын жагсаалт
            var categories = new List<Category>
            {
                new Category { Name = "Food", Description = "Food items" },
                new Category { Name = "Beverages", Description = "Drinks and beverages" },
                new Category { Name = "Snacks", Description = "Snacks and chips" },
                new Category { Name = "Household", Description = "Household items" }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            // Жишээ бүтээгдэхүүнүүд
            var products = new List<Product>
            {
                new Product { 
                    Code = "F001", 
                    Name = "Bread", 
                    Description = "Fresh bread", 
                    Price = 2.50m, 
                    StockQuantity = 5, 
                    CategoryId = categories[0].Id,
                    ImagePath = "C:\\Users\\Dell\\Desktop\\lesson\\Junior\\windows\\Pos\\PosApplication\\ProductImages\\/bread.jpg"
                },
                new Product { 
                    Code = "F002", 
                    Name = "Milk", 
                    Description = "Fresh milk", 
                    Price = 3.99m, 
                    StockQuantity = 30, 
                    CategoryId = categories[0].Id,
                    ImagePath = "C:\\Users\\Dell\\Desktop\\lesson\\Junior\\windows\\Pos\\PosApplication\\ProductImages\\/milk.jpg"
                },
                new Product { 
                    Code = "B001", 
                    Name = "Cola", 
                    Description = "Carbonated drink", 
                    Price = 1.99m, 
                    StockQuantity = 100, 
                    CategoryId = categories[1].Id,
                    ImagePath = "C:\\Users\\Dell\\Desktop\\lesson\\Junior\\windows\\Pos\\PosApplication\\ProductImages\\/cola.jpg"
                },
                new Product { 
                    Code = "B002", 
                    Name = "Water", 
                    Description = "Mineral water", 
                    Price = 0.99m, 
                    StockQuantity = 150, 
                    CategoryId = categories[1].Id,
                    ImagePath = "C:\\Users\\Dell\\Desktop\\lesson\\Junior\\windows\\Pos\\PosApplication\\ProductImages\\/water.jpg"
                },
                new Product { 
                    Code = "S001", 
                    Name = "Chips", 
                    Description = "Potato chips", 
                    Price = 1.50m, 
                    StockQuantity = 75, 
                    CategoryId = categories[2].Id,
                    ImagePath = "C:\\Users\\Dell\\Desktop\\lesson\\Junior\\windows\\Pos\\PosApplication\\ProductImages\\/chips.jpg"
                },
                new Product { 
                    Code = "S002", 
                    Name = "Cookies", 
                    Description = "Chocolate cookies", 
                    Price = 2.25m, 
                    StockQuantity = 60, 
                    CategoryId = categories[2].Id,
                    ImagePath = "C:\\Users\\Dell\\Desktop\\lesson\\Junior\\windows\\Pos\\PosApplication\\ProductImages\\/cookies.jpg"
                },
                new Product { 
                    Code = "H001", 
                    Name = "Soap", 
                    Description = "Hand soap", 
                    Price = 4.99m, 
                    StockQuantity = 40, 
                    CategoryId = categories[3].Id,
                    ImagePath = "C:\\Users\\Dell\\Desktop\\lesson\\Junior\\windows\\Pos\\PosApplication\\ProductImages\\/soap.jpg"
                },
                new Product { 
                    Code = "H002", 
                    Name = "Detergent", 
                    Description = "Laundry detergent", 
                    Price = 8.99m, 
                    StockQuantity = 25, 
                    CategoryId = categories[3].Id,
                    ImagePath = "C:\\Users\\Dell\\Desktop\\lesson\\Junior\\windows\\Pos\\PosApplication\\ProductImages\\/detergent.jpg"
                }
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }
} 