using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wholesale_Clothing_CRM.Models;

namespace Wholesale_Clothing_CRM.Services
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(int id);
        Task<Order> CreateOrderAsync(Order order);
        Task<Order?> UpdateOrderAsync(int id, Order order);
        Task<bool> DeleteOrderAsync(int id);
        Task<List<Order>> SearchOrdersAsync(string query);
    }
}