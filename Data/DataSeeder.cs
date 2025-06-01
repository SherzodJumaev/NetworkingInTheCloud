using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wholesale_Clothing_CRM.Models;

namespace Wholesale_Clothing_CRM.Data
{
    public static class DataSeeder
    {
        public static void SeedData(ApplicationDbContext context)
        {
            var products = new List<Product>
            {
                new Product { Name = "Men's Formal Shirts", Category = "Formal Wear", Price = 89.99m, StockQuantity = 50, SalesCount = 46, Revenue = 2300m },
                new Product { Name = "Women's Casual Dresses", Category = "Casual Wear", Price = 75.50m, StockQuantity = 30, SalesCount = 17, Revenue = 850m },
                new Product { Name = "Men's Jeans", Category = "Casual Wear", Price = 95.00m, StockQuantity = 45, SalesCount = 28, Revenue = 1400m },
                new Product { Name = "Women's Blazers", Category = "Formal Wear", Price = 120.00m, StockQuantity = 25, SalesCount = 15, Revenue = 900m },
                new Product { Name = "Casual T-Shirts", Category = "Casual Wear", Price = 25.99m, StockQuantity = 100, SalesCount = 85, Revenue = 1200m },
                new Product { Name = "Women's Skirts", Category = "Formal Wear", Price = 55.00m, StockQuantity = 35, SalesCount = 22, Revenue = 660m },
                new Product { Name = "Men's Polo Shirts", Category = "Casual Wear", Price = 45.99m, StockQuantity = 60, SalesCount = 33, Revenue = 800m },
                new Product { Name = "Women's Pants", Category = "Casual Wear", Price = 65.00m, StockQuantity = 40, SalesCount = 25, Revenue = 975m },
                new Product { Name = "Unisex Blazers", Category = "Casual Wear", Price = 11.32m, StockQuantity = 13, SalesCount = 10, Revenue = 113.2m },
                new Product { Name = "Women's Coats", Category = "Sportswear", Price = 101.41m, StockQuantity = 30, SalesCount = 27, Revenue = 2738.07m },
                new Product { Name = "Unisex Skirts", Category = "Sportswear", Price = 76.62m, StockQuantity = 81, SalesCount = 76, Revenue = 5823.12m },
                new Product { Name = "Women's Sweaters", Category = "Sportswear", Price = 185.66m, StockQuantity = 28, SalesCount = 14, Revenue = 2599.24m },
                new Product { Name = "Unisex Pants", Category = "Sportswear", Price = 113.42m, StockQuantity = 40, SalesCount = 16, Revenue = 1814.72m },
                new Product { Name = "Women's Jeans", Category = "Outerwear", Price = 58.04m, StockQuantity = 87, SalesCount = 26, Revenue = 1509.04m },
                new Product { Name = "Women's Jeans", Category = "Outerwear", Price = 14.97m, StockQuantity = 66, SalesCount = 27, Revenue = 404.19m },
                new Product { Name = "Men's Sweaters", Category = "Formal Wear", Price = 55.36m, StockQuantity = 78, SalesCount = 41, Revenue = 2269.76m },
                new Product { Name = "Unisex Coats", Category = "Formal Wear", Price = 78.88m, StockQuantity = 40, SalesCount = 16, Revenue = 1262.08m },
                new Product { Name = "Women's Coats", Category = "Outerwear", Price = 72.06m, StockQuantity = 30, SalesCount = 1, Revenue = 72.06m },
                new Product { Name = "Women's Dresses", Category = "Casual Wear", Price = 56.87m, StockQuantity = 50, SalesCount = 50, Revenue = 2843.5m },
                new Product { Name = "Men's Dresses", Category = "Sportswear", Price = 133.0m, StockQuantity = 15, SalesCount = 0, Revenue = 0.0m },
                new Product { Name = "Women's Sweaters", Category = "Outerwear", Price = 18.96m, StockQuantity = 82, SalesCount = 23, Revenue = 436.08m },
                new Product { Name = "Unisex Sweaters", Category = "Sportswear", Price = 94.5m, StockQuantity = 45, SalesCount = 25, Revenue = 2362.5m },
                new Product { Name = "Unisex Sweaters", Category = "Outerwear", Price = 99.97m, StockQuantity = 63, SalesCount = 60, Revenue = 5998.2m },
                new Product { Name = "Women's Sweaters", Category = "Formal Wear", Price = 51.81m, StockQuantity = 42, SalesCount = 39, Revenue = 2020.59m },
                new Product { Name = "Men's Shirts", Category = "Outerwear", Price = 88.25m, StockQuantity = 34, SalesCount = 15, Revenue = 1323.75m },
                new Product { Name = "Women's Jackets", Category = "Outerwear", Price = 139.55m, StockQuantity = 89, SalesCount = 50, Revenue = 6977.5m },
                new Product { Name = "Unisex Dresses", Category = "Casual Wear", Price = 115.75m, StockQuantity = 93, SalesCount = 55, Revenue = 6366.25m },
                new Product { Name = "Men's Jeans", Category = "Casual Wear", Price = 58.38m, StockQuantity = 93, SalesCount = 87, Revenue = 5079.06m },
                new Product { Name = "Women's Shirts", Category = "Casual Wear", Price = 70.7m, StockQuantity = 47, SalesCount = 44, Revenue = 3110.8m },
                new Product { Name = "Women's Shirts", Category = "Outerwear", Price = 112.9m, StockQuantity = 89, SalesCount = 10, Revenue = 1129.0m },
                new Product { Name = "Men's Sweaters", Category = "Outerwear", Price = 136.43m, StockQuantity = 72, SalesCount = 52, Revenue = 7094.36m },
                new Product { Name = "Women's Jeans", Category = "Casual Wear", Price = 194.6m, StockQuantity = 36, SalesCount = 27, Revenue = 5254.2m },
                new Product { Name = "Men's Blazers", Category = "Outerwear", Price = 35.24m, StockQuantity = 24, SalesCount = 12, Revenue = 422.88m },
                new Product { Name = "Women's Pants", Category = "Formal Wear", Price = 23.3m, StockQuantity = 66, SalesCount = 66, Revenue = 1537.8m },
                new Product { Name = "Women's Blazers", Category = "Formal Wear", Price = 134.49m, StockQuantity = 33, SalesCount = 2, Revenue = 268.98m },
                new Product { Name = "Unisex T-Shirts", Category = "Formal Wear", Price = 173.5m, StockQuantity = 76, SalesCount = 51, Revenue = 8848.5m },
                new Product { Name = "Men's T-Shirts", Category = "Sportswear", Price = 24.44m, StockQuantity = 28, SalesCount = 9, Revenue = 219.96m },
                new Product { Name = "Men's Coats", Category = "Casual Wear", Price = 24.76m, StockQuantity = 57, SalesCount = 49, Revenue = 1213.24m },
                new Product { Name = "Women's Skirts", Category = "Outerwear", Price = 160.15m, StockQuantity = 88, SalesCount = 3, Revenue = 480.45m },
                new Product { Name = "Men's Jeans", Category = "Sportswear", Price = 129.2m, StockQuantity = 23, SalesCount = 18, Revenue = 2325.6m },
                new Product { Name = "Women's T-Shirts", Category = "Formal Wear", Price = 15.9m, StockQuantity = 20, SalesCount = 15, Revenue = 238.5m },
                new Product { Name = "Unisex Shirts", Category = "Formal Wear", Price = 162.1m, StockQuantity = 10, SalesCount = 2, Revenue = 324.2m },
                new Product { Name = "Men's Dresses", Category = "Formal Wear", Price = 152.77m, StockQuantity = 37, SalesCount = 36, Revenue = 5499.72m },
                new Product { Name = "Men's T-Shirts", Category = "Formal Wear", Price = 194.19m, StockQuantity = 27, SalesCount = 11, Revenue = 2136.09m },
                new Product { Name = "Women's Jackets", Category = "Formal Wear", Price = 153.39m, StockQuantity = 70, SalesCount = 16, Revenue = 2454.24m },
                new Product { Name = "Women's Coats", Category = "Sportswear", Price = 55.99m, StockQuantity = 63, SalesCount = 39, Revenue = 2183.61m },
                new Product { Name = "Men's Blazers", Category = "Casual Wear", Price = 166.48m, StockQuantity = 89, SalesCount = 63, Revenue = 10488.24m },
                new Product { Name = "Unisex Shirts", Category = "Outerwear", Price = 68.51m, StockQuantity = 99, SalesCount = 33, Revenue = 2260.83m },
                new Product { Name = "Men's Jackets", Category = "Formal Wear", Price = 138.46m, StockQuantity = 24, SalesCount = 4, Revenue = 553.84m },
                new Product { Name = "Men's Jeans", Category = "Casual Wear", Price = 86.76m, StockQuantity = 10, SalesCount = 2, Revenue = 173.52m },
                new Product { Name = "Unisex Shirts", Category = "Sportswear", Price = 108.88m, StockQuantity = 98, SalesCount = 58, Revenue = 6315.04m },
            };
            context.Products.AddRange(products);
            context.SaveChanges();
            System.Console.WriteLine("================= Products Seeded...===================");

            var customers = new List<Customer>
            {
                new Customer { FirstName = "John", LastName = "Doe", Email = "john.doe@email.com", Phone = "555-0101", CreatedAt = DateTime.UtcNow.AddDays(-30) },
                new Customer { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@email.com", Phone = "555-0102", CreatedAt = DateTime.UtcNow.AddDays(-25) },
                new Customer { FirstName = "Mike", LastName = "Johnson", Email = "mike.j@email.com", Phone = "555-0103", CreatedAt = DateTime.UtcNow.AddDays(-20) },
                new Customer { FirstName = "Sarah", LastName = "Wilson", Email = "sarah.w@email.com", Phone = "555-0104", CreatedAt = DateTime.UtcNow.AddDays(-15) },
                new Customer { FirstName = "David", LastName = "Brown", Email = "david.b@email.com", Phone = "555-0105", CreatedAt = DateTime.UtcNow.AddDays(-10) },
                new Customer { FirstName = "Emily", LastName = "Davis", Email = "emily.d@email.com", Phone = "555-0106", CreatedAt = DateTime.UtcNow.AddDays(-5) },
                new Customer { FirstName = "Robert", LastName = "Miller", Email = "robert.m@email.com", Phone = "555-0107", CreatedAt = DateTime.UtcNow.AddDays(-3) },
                new Customer { FirstName = "Lisa", LastName = "Anderson", Email = "lisa.a@email.com", Phone = "555-0108", CreatedAt = DateTime.UtcNow.AddDays(-2) },
                new Customer { FirstName = "Mark", LastName = "Taylor", Email = "mark.t@email.com", Phone = "555-0109", CreatedAt = DateTime.UtcNow.AddDays(-1) },
                new Customer { FirstName = "Anna", LastName = "White", Email = "anna.w@email.com", Phone = "555-0110", CreatedAt = DateTime.UtcNow },
                new Customer { FirstName = "Chris", LastName = "Garcia", Email = "chris.g@email.com", Phone = "555-0111", CreatedAt = DateTime.UtcNow },
                new Customer { FirstName = "Maria", LastName = "Lopez", Email = "maria.l@email.com", Phone = "555-0112", CreatedAt = DateTime.UtcNow }
            };

            context.Customers.AddRange(customers);
            context.SaveChanges();
            System.Console.WriteLine("================= Customers Seeded...===================");

            // Create orders
            var random = new Random();
            var orders = new List<Order>();

            for (int i = 0; i < 27; i++)
            {
                var customer = customers[random.Next(customers.Count)];
                var orderDate = DateTime.UtcNow.AddDays(-random.Next(30));

                var order = new Order
                {
                    CustomerId = customer.Id,
                    OrderDate = orderDate,
                    Status = random.Next(10) > 8 ? "Pending" : "Completed"
                };

                var orderItems = new List<OrderItem>();
                var itemCount = random.Next(1, 4); // 1-3 items per order

                for (int j = 0; j < itemCount; j++)
                {
                    var product = products[random.Next(products.Count)];
                    var quantity = random.Next(1, 3);

                    orderItems.Add(new OrderItem
                    {
                        ProductId = product.Id,
                        Quantity = quantity,
                        UnitPrice = product.Price
                    });
                }

                order.OrderItems = orderItems;
                order.TotalAmount = orderItems.Sum(oi => oi.Quantity * oi.UnitPrice);
                orders.Add(order);
            }

            context.Orders.AddRange(orders);
            context.SaveChanges();

            System.Console.WriteLine("================= OrderItems Seeded...===================");
        }
    }
}