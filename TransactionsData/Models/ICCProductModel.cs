using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionsData.Models
{
    public class ICCProductModel
    {
        [Key]
        public int id { get; set; }
        public string SupplierID { get; set; }
        public string ProductCode { get; set; }        
        public string ProductName { get; set; }
        public string Status { get; set; }
        public int TopupLevel { get; set; }
        public string Information { get; set; }

        //public ICollection<ICCProductStockLevels> stock { get; set; }

    }
}
