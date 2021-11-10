using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabariPay.ViewModels
{
    public class InvoiceViewModel
    {
        public int CustomerId { get; set; }
        public string ItemType { get; set; }
        public decimal Price { get; set; }
    }

    public enum ItemTypeEnum
    {
        Groceries, Others
    }
}
