using DutchTreat.Data.Entities;

using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            var orders = context.Orders;
            if (includeItems) return orders
                .Include(o => o.Items)
                .ThenInclude(p => p.Product); 
            return orders.ToList(); ;
        }

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            var userOrders = context.Orders.Where(x => x.User.UserName == username);
            if (includeItems) return userOrders
                .Include(o => o.Items)
                .ThenInclude(p => p.Product);
            return userOrders.ToList();
        }

        public Order GetOrderById(string username, int id)
        {
            try
            {
                return context.Orders
                    .Include(o => o.Items).
                    ThenInclude(p => p.Product)
                    .FirstOrDefault(o => o.Id == id && o.User.UserName == username);
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get all products: {ex}");
                return null;
            }
        }

        public void AddEntity(object model)
        {
            context.Add(model);
        }
    }
}
