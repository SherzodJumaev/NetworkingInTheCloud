using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wholesale_Clothing_CRM.DTOs;

namespace Wholesale_Clothing_CRM.Services
{
    public interface IDashboardService
    {
        Task<DashboardDto> GetDashboardDataAsync();
    }
}