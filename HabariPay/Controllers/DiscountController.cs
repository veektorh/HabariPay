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
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountRepository discountRepository;

        public DiscountController(IDiscountRepository discountRepository)
        {
            this.discountRepository = discountRepository;
        }

        public async Task<IActionResult> Get()
        {
            var response = new ApiResponse<List<Discount>>();            
            var discounts = await discountRepository.GetDiscounts();
            if (discounts == null)
            {
                throw new AppException("discount not found");
            }
            response.Message = "success";
            response.Status = true;
            response.Data = discounts;
            return Ok(response);
        }

        [Route("{type}")]
        public async Task<IActionResult> GetByType(string type)
        {
            var response = new ApiResponse<Discount>();
            
            var discount = await discountRepository.GetDiscountByType(type);
            if (discount == null)
            {
                throw new AppException("discount not found");
            }

            response.Message = "success";
            response.Status = true;
            response.Data = discount;
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateDiscountModel model)
        {
            var response = new ApiResponse<Discount>();

            var exist = await discountRepository.GetDiscountByType(model.Type);
            if (exist != null)
            {
                throw new AppException("discount already exist");
            }

            if (model.Percentage < 1)
            {
                throw new AppException("percentage must be greater than 0");
            }

            var newdiscount = new Discount() { Type = model.Type, Percentage = model.Percentage };
            var discount = await discountRepository.CreateDiscount(newdiscount);

            response.Message = "success";
            response.Status = true;
            response.Data = discount;
            return Ok(response);
        }
    }
}
