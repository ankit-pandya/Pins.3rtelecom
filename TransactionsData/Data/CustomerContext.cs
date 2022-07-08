using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionsData.Models;

namespace TransactionsData.Data
{
    public class CustomerContext :DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> option) : base(option)
        {
                
        }

        public DbSet<MerchantModel> Merchants { get; set; }
    }
}
