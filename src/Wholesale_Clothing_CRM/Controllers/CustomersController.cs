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
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        // [HttpPost]
        // public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        // {
        //     var createdCustomer = await _customerService.CreateCustomerAsync(customer);
        //     return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.Id }, createdCustomer);
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        // {
        //     var updatedCustomer = await _customerService.UpdateCustomerAsync(id, customer);
        //     if (updatedCustomer == null) return NotFound();
        //     return Ok(updatedCustomer);
        // }

        [HttpGet("search")]
        public async Task<ActionResult<List<Customer>>> SearchProducts([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                var allCustomers = await _customerService.GetAllCustomersAsync();
                return Ok(allCustomers);
            }

            var customers = await _customerService.SearchCustomersAsync(query);
            return Ok(customers);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var deleted = await _customerService.DeleteCustomerAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}