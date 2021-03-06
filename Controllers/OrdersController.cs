using AutoMapper;

using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : ControllerBase
    {
        private readonly IDutchRepository repository;
        private readonly ILogger<OrdersController> logger;
        private readonly IMapper mapper;
        private readonly UserManager<StoreUser> userManager;

        public OrdersController(IDutchRepository repository,
            ILogger<OrdersController> logger,
            IMapper mapper, 
            UserManager<StoreUser> userManager)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name;
                var result = repository.GetAllOrdersByUser(username,includeItems);
                return Ok(mapper.Map<IEnumerable<OrderViewModel>>(result));
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
                var order = repository.GetOrderById(User.Identity.Name,  id);
                if (order != null) return Ok(mapper.Map<Order, OrderViewModel>(order));
                return NotFound();
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to get order {id} :{ex}");
                return BadRequest($"Failed to get order {id}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderViewModel model)
        {
            try
            {
                var newOrder = mapper.Map<Order>(model);
                if (newOrder.OrderDate == DateTime.MinValue)
                {
                    newOrder.OrderDate = DateTime.Now;
                }
                var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
                newOrder.User = currentUser;

                repository.AddEntity(newOrder);
                if (repository.SaveAll())
                {
                    return Created($"/api/orders/{newOrder.Id}", mapper.Map<OrderViewModel>(newOrder));
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
