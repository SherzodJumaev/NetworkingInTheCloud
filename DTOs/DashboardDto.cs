using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wholesale_Clothing_CRM.DTOs
{
    public class DashboardDto
    {
        public decimal TodaysSales { get; set; }
        public decimal TodaysSalesChange { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalOrdersChange { get; set; }
        public int ItemsSold { get; set; }
        public decimal ItemsSoldChange { get; set; }
        public int NewCustomers { get; set; }
        public decimal NewCustomersChange { get; set; }
        public List<TopProductDto> TopProducts { get; set; } = new List<TopProductDto>();
    }
}