using HabariPay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabariPay.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShopContext shopContext;

        public CustomerRepository(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            return await shopContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetCustomerByName(string name)
        {
            return await shopContext.Customers.FirstOrDefaultAsync(a => a.Name.ToLower() == name.ToLower());
        }

        public async Task<Customer> GetCustomerById(int Id)
        {
            return await shopContext.Customers.FirstOrDefaultAsync(a => a.Id == Id);
        }

        public async Task<Customer> CreateCustomer(Customer customer)
        {
            var res = await shopContext.Customers.AddAsync(customer);
            await shopContext.SaveChangesAsync();
            return res.Entity;
        }
    }
}
