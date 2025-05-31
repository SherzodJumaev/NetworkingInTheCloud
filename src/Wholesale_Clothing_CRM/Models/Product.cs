using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wholesale_Clothing_CRM.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public int SalesCount { get; set; }
        public decimal Revenue { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}