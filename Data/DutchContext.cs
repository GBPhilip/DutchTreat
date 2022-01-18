using DutchTreat.Data.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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
    }
}
