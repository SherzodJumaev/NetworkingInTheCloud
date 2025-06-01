using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wholesale_Clothing_CRM.Data;
using Wholesale_Clothing_CRM.Models;

namespace Wholesale_Clothing_CRM.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<List<Order>> SearchOrdersAsync(string query)
        {
            query = query.ToLower().Trim();

            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .Where(o =>
                    o.Customer.FirstName.ToLower().Contains(query) ||
                    o.Customer.LastName.ToLower().Contains(query) ||
                    (o.Customer.FirstName + " " + o.Customer.LastName).ToLower().Contains(query) ||
                    o.Status.ToLower().Contains(query) ||
                    o.Id.ToString().Contains(query)
                // Add more searchable fields as needed
                )
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> UpdateOrderAsync(int id, Order order)
        {
            var existingOrder = await _context.Orders.FindAsync(id);
            if (existingOrder == null) return null;

            existingOrder.Status = order.Status;
            existingOrder.TotalAmount = order.TotalAmount;

            await _context.SaveChangesAsync();
            return existingOrder;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}