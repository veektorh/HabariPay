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
    public class InvoiceController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IDiscountRepository discountRepository;

        public InvoiceController(ICustomerRepository customerRepository, IDiscountRepository discountRepository)
        {
            this.customerRepository = customerRepository;
            this.discountRepository = discountRepository;
        }

        [Route("amount")]
        public async Task<IActionResult> GetInvoiceAmount(InvoiceViewModel model)
        {
            var response = new ApiResponse<object>();

            var customer = await customerRepository.GetCustomerById(model.CustomerId);
            if (customer == null)
            {
                throw new AppException("Incorrect customer id");
            }

            if (model.Price < 1)
            {
                throw new AppException("prices must be greater than 0");
            }
            var discountAmount = 0m;
            var cash_discount = model.Price / 100;
            var cash_discount_amount = 1;

            if (cash_discount > 0)
            {
                cash_discount_amount *= 5;
            }

            if (model.ItemType.Equals(ItemTypeEnum.Groceries.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                discountAmount = 0;
            }
            else if (customer.Role.Equals(RoleEnum.Affiliate.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                var discount = await discountRepository.GetDiscountByType(RoleEnum.Affiliate.ToString());
                discountAmount = (discount.Percentage/100) * model.Price;
            }

            else if (customer.Role.Equals(RoleEnum.Employee.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                var discount = await discountRepository.GetDiscountByType(RoleEnum.Affiliate.ToString());
                discountAmount = (discount.Percentage / 100) * model.Price;
            }
            else if ( ( DateTime.Now - customer.CreatedAt).TotalDays > 730)
            {
                discountAmount = (5 / 100) * model.Price;
            }


            var totalDiscount = cash_discount_amount + discountAmount;
            var total_Invoice_Amount = model.Price - totalDiscount;

            total_Invoice_Amount = total_Invoice_Amount < 0 ? 0 : total_Invoice_Amount;

            response.Message = "success";
            response.Status = true;
            response.Data = new { TotalInvoiceAmount = total_Invoice_Amount };
            return Ok(response);
        }
    }
}
