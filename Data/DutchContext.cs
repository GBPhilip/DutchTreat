using DutchTreat.Data.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using System;

namespace DutchTreat.Data
{
    public class DutchContext : DbContext
    {
        private readonly IConfiguration config;

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DutchContext(IConfiguration config)
        {
            this.config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(config["ConnectionStrings:DutchContextDB"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(new Order()
            {
                Id = 1,
                OrderDate = DateTime.UtcNow,
                OrderNumber = "122345"
            });
        }
    }
}
