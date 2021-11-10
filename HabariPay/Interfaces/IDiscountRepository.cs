using HabariPay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HabariPay.Repository
{
    public interface IDiscountRepository
    {
        Task<Discount> CreateDiscount(Discount discount);
        Task<Discount> GetDiscountByType(string type);
        Task<List<Discount>> GetDiscounts();
    }
}