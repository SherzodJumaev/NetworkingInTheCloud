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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }
        
        [HttpGet("search")]
        public async Task<ActionResult<List<Product>>> SearchProducts([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                var allProducts = await _productService.GetAllProductsAsync();
                return Ok(allProducts);
            }

            var products = await _productService.SearchProductsAsync(query);
            return Ok(products);
        }
        // [HttpPost]
        // public async Task<ActionResult<Product>> CreateProduct(Product product)
        // {
        //     var createdProduct = await _productService.CreateProductAsync(product);
        //     return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        // }

        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateProduct(int id, Product product)
        // {
        //     var updatedProduct = await _productService.UpdateProductAsync(id, product);
        //     if (updatedProduct == null) return NotFound();
        //     return Ok(updatedProduct);
        // }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _productService.DeleteProductAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}