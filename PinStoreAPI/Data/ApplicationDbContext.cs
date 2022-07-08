using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PinStoreAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {
        }

        public DbSet<MerchantModel> Merchants { get; set; }
        public DbSet<MerchantTerminals> MerchantTerminal { get; set; }
        public DbSet<TransactionsModel> Transactions { get; set; }
        public DbSet<ICCProviderModel> ICCProviders { get; set; }
        public DbSet<ICCProductModel> ICCProducts { get; set; }        
        public DbSet<ICCProductPins> ICCProductPins { get; set; }
        public DbSet<ICCProductStockLevels> ICCProductStockLevels { get; set; }
        public DbSet<ProviderModel> tblproviders { get; set; }
        public DbSet<ProductModel> tblproducts { get; set; }
        public DbSet<ProductDenominations> tblproductdenoms { get; set; }
        public DbSet<ProductPinsModel> tblproductpins { get; set; }
        public DbSet<MTProductModel> tblMTProducts { get; set; }
        public DbSet<MerchantBalance> tblMerchantBalance { get; set; }
    }
}
