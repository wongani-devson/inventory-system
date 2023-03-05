using inventory_api.Models;
using Microsoft.AspNetCore.Mvc;

namespace inventory_api.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer> GetCustomer(int id);
        Task<Customer> CreateCustomer(Customer customer);
        Task<Customer> UpdateCustomer(int id, Customer customer);
        Task DeleteCustomer(int id);

    }
}
