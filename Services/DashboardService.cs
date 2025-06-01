using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Wholesale_Clothing_CRM.Data;
using Wholesale_Clothing_CRM.DTOs;

namespace Wholesale_Clothing_CRM.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboardDataAsync()
        {
            var today = DateTime.UtcNow.Date;
            var yesterday = today.AddDays(-1);

            // Today's sales
            var todaysSales = await _context.Orders
                .Where(o => o.OrderDate.Date == today && o.Status == "Completed")
                .SumAsync(o => o.TotalAmount);

            var yesterdaysSales = await _context.Orders
                .Where(o => o.OrderDate.Date == yesterday && o.Status == "Completed")
                .SumAsync(o => o.TotalAmount);

            var salesChange = yesterdaysSales > 0 ? ((todaysSales - yesterdaysSales) / yesterdaysSales * 100) : 0;

            // Total orders
            var totalOrders = await _context.Orders.CountAsync();
            var lastMonthOrders = await _context.Orders
                .Where(o => o.OrderDate >= today.AddDays(-30))
                .CountAsync();
            var previousMonthOrders = await _context.Orders
                .Where(o => o.OrderDate >= today.AddDays(-60) && o.OrderDate < today.AddDays(-30))
                .CountAsync();

            var ordersChange = previousMonthOrders > 0 ? ((decimal)(lastMonthOrders - previousMonthOrders) / previousMonthOrders * 100) : 0;

            // Items sold
            var itemsSold = await _context.OrderItems
                .Where(oi => oi.Order.OrderDate.Date == today)
                .SumAsync(oi => oi.Quantity);

            var yesterdayItemsSold = await _context.OrderItems
                .Where(oi => oi.Order.OrderDate.Date == yesterday)
                .SumAsync(oi => oi.Quantity);

            var itemsChange = yesterdayItemsSold > 0 ? ((decimal)(itemsSold - yesterdayItemsSold) / yesterdayItemsSold * 100) : 0;

            // New customers
            var newCustomers = await _context.Customers
                .Where(c => c.CreatedAt.Date == today)
                .CountAsync();

            var yesterdayNewCustomers = await _context.Customers
                .Where(c => c.CreatedAt.Date == yesterday)
                .CountAsync();

            var customersChange = yesterdayNewCustomers > 0 ? ((decimal)(newCustomers - yesterdayNewCustomers) / yesterdayNewCustomers * 100) : 0;

            // Top products
            var totalSales = await _context.Products.SumAsync(p => p.SalesCount);
            var topProducts = await _context.Products
                .OrderByDescending(p => p.SalesCount)
                .Take(10)
                .Select(p => new TopProductDto
                {
                    Name = p.Name,
                    Category = p.Category,
                    SalesPercentage = totalSales > 0 ? (p.SalesCount * 100 / totalSales) : 0,
                    Revenue = p.Revenue
                })
                .ToListAsync();

            for (int i = 0; i < topProducts.Count; i++)
            {
                topProducts[i].Rank = i + 1;
            }

            return new DashboardDto
            {
                TodaysSales = todaysSales,
                TodaysSalesChange = salesChange,
                TotalOrders = totalOrders,
                TotalOrdersChange = ordersChange,
                ItemsSold = itemsSold,
                ItemsSoldChange = itemsChange,
                NewCustomers = newCustomers,
                NewCustomersChange = customersChange,
                TopProducts = topProducts
            };
        }
    }
}