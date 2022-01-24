using DutchTreat.Data.Entities;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;
using System.Linq;


namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext context;
        private readonly ILogger<DutchContext> logger;

        public DutchRepository(DutchContext context, ILogger<DutchContext> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            logger.LogInformation("Get All Products was called");
            return context.Products.ToList(); ;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return context.Products.Where(p => p.Category == category).ToList(); ;
        }

        public bool SaveAll()
        {

            return context.SaveChanges() != 0;
        }
    }
}
