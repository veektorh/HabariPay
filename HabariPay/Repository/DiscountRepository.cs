using HabariPay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabariPay.Repository
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ShopContext shopContext;

        public DiscountRepository(ShopContext shopContext)
        {
            this.shopContext = shopContext;
        }

        public async Task<List<Discount>> GetDiscounts()
        {
            return await shopContext.Discounts.ToListAsync();
        }

        public async Task<Discount> GetDiscountByType(string type)
        {
            return await shopContext.Discounts.FirstOrDefaultAsync(a => a.Type.ToLower() == type.ToLower());
        }


        public async Task<Discount> CreateDiscount(Discount discount)
        {
            var res = await shopContext.Discounts.AddAsync(discount);
            await shopContext.SaveChangesAsync();
            return res.Entity;
        }
    }
}
