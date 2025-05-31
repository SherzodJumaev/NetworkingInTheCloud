using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wholesale_Clothing_CRM.DTOs
{
    public class TopProductDto
    {
        public int Rank { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int SalesPercentage { get; set; }
        public decimal Revenue { get; set; }
    }
}