using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabariPay.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public enum RoleEnum
    {
        Affiliate,Employee
    }
}
