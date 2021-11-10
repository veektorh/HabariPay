using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabariPay.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public decimal Percentage { get; set; }
    }
}
