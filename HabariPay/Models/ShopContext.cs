using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabariPay.Models
{
    public class ShopContext : DbContext
    {
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public ShopContext(DbContextOptions<ShopContext> options) : base(options)
        {
        }
    }
}
