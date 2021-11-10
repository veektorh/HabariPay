using HabariPay.Controllers;
using HabariPay.Helper;
using HabariPay.Models;
using HabariPay.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabariPay.Test
{
    class CustomerControllerTest
    {
         
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAllCustomersTest_ReturnsListOfCustomers()
        {
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            mockCustomerRepo.Setup(repo => repo.GetCustomers()).Returns(GetTestCustomers());
            var customerController = new CustomerController(mockCustomerRepo.Object);

            var result = customerController.Get().GetAwaiter().GetResult();

            var okresult = result as OkObjectResult;
            var apiResponse = okresult.Value as ApiResponse<List<Customer>>;
            var data = apiResponse.Data;
            
            Assert.IsNotNull(okresult);
            Assert.AreEqual(200, okresult.StatusCode);
            Assert.IsTrue(okresult is OkObjectResult);
            Assert.AreEqual(2, apiResponse.Data.Count);
        }

        [TestCase("Victor")]
        [TestCase("Daniel")]
        public void GetCustomerByNameTest_ReturnsCustomer(string name)
        {
            
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            mockCustomerRepo.Setup(repo => repo.GetCustomerByName(name)).Returns(GetTestCustomersByName(name));
            var customerController = new CustomerController(mockCustomerRepo.Object);

            var result = customerController.GetByName(name).GetAwaiter().GetResult();

            var okresult = result as OkObjectResult;
            var apiResponse = okresult.Value as ApiResponse<Customer>;
            var data = apiResponse.Data;

            Assert.IsNotNull(okresult);
            Assert.AreEqual(200, okresult.StatusCode);
            Assert.IsTrue(okresult is OkObjectResult);
            Assert.AreEqual(name, apiResponse.Data.Name);
        }

        private async Task<List<Customer>> GetTestCustomers()
        {
            var customerList = new List<Customer>();
            customerList.Add(new Customer
            {
                Id = 1,
                Name = "Victor",
                Role = "Affiliate",
                CreatedAt = DateTime.Now
            });

            customerList.Add(new Customer
            {
                Id = 2,
                Name = "Daniel",
                Role = "Employee",
                CreatedAt = DateTime.Now
            });

            return customerList;
        }

        private async Task<Customer> GetTestCustomersByName(string name)
        {
            var customerList = GetTestCustomers().Result;
            return customerList.FirstOrDefault(a => a.Name == name);
        }
    }
}
