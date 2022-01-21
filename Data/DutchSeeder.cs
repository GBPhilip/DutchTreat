using DutchTreat.Data.Entities;

using Microsoft.AspNetCore.Hosting;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext context;
        private readonly IWebHostEnvironment environment;

        public DutchSeeder(DutchContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }


        public void Seed()
        {
            context.Database.EnsureCreated();
            if (!context.Products.Any())
            {
                var filePath = Path.Combine(environment.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                context.Products.AddRange(products);
                var order = new Order()
                {
                    OrderDate = DateTime.Today,
                    OrderNumber = "10000",
                    Items = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    }
                };
                context.Orders.Add(order);
                context.SaveChanges();
            }
        }
    }
}
