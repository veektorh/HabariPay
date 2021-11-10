using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabariPay.Models
{
    public class Invoice
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int DiscountId { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
    }
}
