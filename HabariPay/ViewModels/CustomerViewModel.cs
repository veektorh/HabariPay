using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HabariPay.ViewModels
{
    public class CustomerViewModel
    {
    }

    public class CreateCustomerModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
