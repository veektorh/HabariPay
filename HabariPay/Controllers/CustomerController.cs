using HabariPay.Helper;
using HabariPay.Models;
using HabariPay.Repository;
using HabariPay.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabariPay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<IActionResult> Get()
        {
            var response = new ApiResponse<List<Customer>>();
            var customers = await customerRepository.GetCustomers();
            if (customers == null)
            {
                throw new AppException("customer not found");
            }

            response.Message = "success";
            response.Status = true;
            response.Data = customers;
            return Ok(response);
        }

        [Route("name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = new ApiResponse<Customer>();

            var customer = await customerRepository.GetCustomerByName(name);
            if (customer == null)
            {
                throw new AppException("customer not found");
            }

            response.Message = "success";
            response.Status = true;
            response.Data = customer;
            return Ok(response);
        }

        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = new ApiResponse<Customer>();
            
            var customer = await customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                throw new AppException("customer not found");
            }

            response.Message = "success";
            response.Status = true;
            response.Data = customer;
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateCustomerModel model)
        {
            var response = new ApiResponse<Customer>();

            var exist = await customerRepository.GetCustomerByName(model.Name);
            if (exist != null)
            {
                throw new AppException("customer already exist");
            }

            var roles = new List<string> { RoleEnum.Affiliate.ToString(), RoleEnum.Employee.ToString() };

            if (!roles.Contains(model.Role))
            {
                throw new AppException($"Invalid role, Allowed values are {string.Join(",",roles)}");
            }
            var newcustomer = new Customer() { Name = model.Name, Role = model.Role };
            var customer = await customerRepository.CreateCustomer(newcustomer);

            response.Message = "success";
            response.Status = true;
            response.Data = customer;
            return Ok(response);
        }
    }
}
