using HabariPay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HabariPay.Repository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomers();
        Task<Customer> GetCustomerByName(string name);
        Task<Customer> GetCustomerById(int Id);
        Task<Customer> CreateCustomer(Customer customer);
    }
}