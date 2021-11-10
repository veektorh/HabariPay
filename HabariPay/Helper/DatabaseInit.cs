using HabariPay.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabariPay.Helper
{
    public class DatabaseInit
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            try
            {
                using (var serviceScope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                    .CreateScope())
                {
                    ShopContext context = serviceScope.ServiceProvider.GetService<ShopContext>();
                    context.Database.Migrate();
                    if (!context.Customers.Any())
                    {
                        context.Customers.Add(new Customer() { Name = "victor" , Role= RoleEnum.Affiliate.ToString(), CreatedAt = DateTime.Now.AddYears(-4) });

                        context.Customers.Add(new Customer() { Name = "tolu", Role = RoleEnum.Affiliate.ToString(), CreatedAt = DateTime.Now.AddYears(-1) });

                        context.Customers.Add(new Customer() { Name = "wale", Role = RoleEnum.Employee.ToString()});
                    }

                    if (!context.Discounts.Any())
                    {
                        context.Discounts.Add(new Discount { Percentage = 10, Type = RoleEnum.Affiliate.ToString() });
                        context.Discounts.Add(new Discount { Percentage = 30, Type = RoleEnum.Employee.ToString() });
                    }

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
