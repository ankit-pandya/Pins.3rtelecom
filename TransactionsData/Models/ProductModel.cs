using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionsData.Models
{
    public class ProductModel
    {
        [Key]
        public int id { get; set; }        
        public string productCode { get; set; }
        public string productName { get; set; }
        public string status { get; set; }
        public int topupLevel { get; set; }
        public string information { get; set; }
        public string providerCode { get; set; }
        public ProviderModel providertbl { get; set; }
    }
}
