using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wholesale_Clothing_CRM.Models;
using Wholesale_Clothing_CRM.Services;

namespace Wholesale_Clothing_CRM.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult> GetOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        // [HttpPost]
        // public async Task<ActionResult<Order>> CreateOrder(Order order)
        // {
        //     var createdOrder = await _orderService.CreateOrderAsync(order);
        //     return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.Id }, createdOrder);
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateOrder(int id, Order order)
        // {
        //     var updatedOrder = await _orderService.UpdateOrderAsync(id, order);
        //     if (updatedOrder == null) return NotFound();
        //     return Ok(updatedOrder);
        // }

        [HttpGet("search")]
        public async Task<ActionResult<List<Order>>> SearchProducts([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                var allOrders = await _orderService.GetAllOrdersAsync();
                return Ok(allOrders);
            }

            var orders = await _orderService.SearchOrdersAsync(query);
            return Ok(orders);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var deleted = await _orderService.DeleteOrderAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}