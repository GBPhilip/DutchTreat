using DutchTreat.Controllers.Data.Entities;
using DutchTreat.Data.Entities;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;

namespace DutchTreat.Data
{
    public class DutchContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DutchContext(DbContextOptions<DutchContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .HasData(new Order
                {
                    Id = 1,
                    OrderDate = DateTime.UtcNow,
                    OrderNumber = "12345"
                });
            //    modelBuilder.Entity<Product>()
            //      .Property(p => p.Price)
            //      .HasColumnType("money");

            //    modelBuilder.Entity<OrderItem>()
            //      .Property(o => o.UnitPrice)
            //      .HasColumnType("money");
        }
    }
}
