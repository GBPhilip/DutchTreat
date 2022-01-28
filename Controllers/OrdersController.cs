using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;

namespace DutchTreat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<OrdersController> logger;

        public OrdersController(IDutchRepository repository, ILogger<OrdersController> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(repository.GetAllOrders());
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get orders {ex}");
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = repository.GetOrderById(id);
                if (order != null) return Ok(order);
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get order {id} :{ex}");
                return BadRequest($"Failed to get order {id}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrderViewModel model)
        {
            try
            {
                var newOrder = new Order()
                {
                    OrderDate = model.OrderDate,
                    OrderNumber = model.OrderNumber,
                    Id = model.OrderId
                };

                if (newOrder.OrderDate == DateTime.MinValue)
                {
                    newOrder.OrderDate = DateTime.Now;
                }
                repository.AddEntity(newOrder);
                if (repository.SaveAll())
                {
                    var viewModel = new OrderViewModel()
                    {
                        OrderId = newOrder.Id,
                        OrderDate = newOrder.OrderDate,
                        OrderNumber = newOrder.OrderNumber,
                    };
                    return Created($"/api/orders/{viewModel.OrderId}", viewModel);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to save a new order:{ex}");
            }
            return BadRequest("Failed to save new order");
        }
    }
}
