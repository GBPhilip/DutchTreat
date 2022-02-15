using DutchTreat.Controllers.Data.Entities;
using DutchTreat.Data.Entities;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext context;
        private readonly IWebHostEnvironment environment;
        private readonly UserManager<StoreUser> userManager;

        public DutchSeeder(DutchContext context, IWebHostEnvironment environment, UserManager<StoreUser> userManager)
        {
            this.context = context;
            this.environment = environment;
            this.userManager = userManager;
        }


        public async Task SeedAsync()
        {
            context.Database.EnsureCreated();
            var user = await userManager.FindByEmailAsync("Philip@dutchtreat.com");

            if (user is null)
            {
                user = new StoreUser()
                {
                    FirstName = "Philip",
                    LastName = "Sutton",
                    Email = "Philip@dutchtreat.com",
                    UserName = "Philip@dutchtreat.com",
                };
                var result = await userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seed");
                }
            }
            if (!context.Products.Any())
            {
                var filePath = Path.Combine(environment.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                context.Products.AddRange(products);
                var order = context.Orders.SingleOrDefault(x => x.Id == 1);
                if (order is not null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    { 
                        new OrderItem
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }

                context.SaveChanges();
            }
        }
    }
}
