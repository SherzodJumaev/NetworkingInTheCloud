using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wholesale_Clothing_CRM.Models;

namespace Wholesale_Clothing_CRM.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer?> UpdateCustomerAsync(int id, Customer customer);
        Task<bool> DeleteCustomerAsync(int id);
        Task<List<Customer>> SearchCustomersAsync(string query);
    }
}