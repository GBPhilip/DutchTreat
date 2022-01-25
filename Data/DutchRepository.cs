using DutchTreat.Data.Entities;

using Microsoft.Extensions.Logging;

using System;
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
            try
            {
                logger.LogInformation("Get All Products was called");
                return context.Products.ToList(); ;

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            try
            {
                return context.Products.Where(p => p.Category == category).ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public bool SaveAll()
        {

            return context.SaveChanges() != 0;
        }
    }
}
