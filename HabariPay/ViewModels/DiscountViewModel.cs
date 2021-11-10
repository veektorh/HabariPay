using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HabariPay.ViewModels
{
    public class DiscountViewModel
    {
    }

    public class CreateDiscountModel
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public decimal Percentage { get; set; }
    }
}
