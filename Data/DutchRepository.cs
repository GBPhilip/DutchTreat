﻿using DutchTreat.Data.Entities;

using System.Collections.Generic;
using System.Linq;


namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext context;

        public DutchRepository(DutchContext context)
        {
            this.context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
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
