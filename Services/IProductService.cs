using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wholesale_Clothing_CRM.Models;

namespace Wholesale_Clothing_CRM.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> GetProductByIdAsync(int id);
        Task<List<Product>> SearchProductsAsync(string query);
        Task<Product> CreateProductAsync(Product product);
        Task<Product?> UpdateProductAsync(int id, Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}