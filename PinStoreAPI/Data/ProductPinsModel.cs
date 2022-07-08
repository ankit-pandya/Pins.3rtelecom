using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinStoreAPI.Data
{
    public class ProductPinsModel
    {
        [Key]        
        public int id { get; set; }
        public string pin { get; set; }
        public string serial { get; set; }        
        public DateTime dateSold { get; set; }
        public string transactionId { get; set; }

        public int productDenominationsid { get; set; }
        public ProductDenominations productDenominations { get; set; }
        
    }
}
