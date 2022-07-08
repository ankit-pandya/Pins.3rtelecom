using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinStoreAPI.Data
{
    public class ProductDenominations
    {
        [Key]
        public int id { get; set; }                
        public string freephoneNumber { get; set; }
        public string generalNumber { get; set; }
        public string londonNumber { get; set; }
        public string helplineNumber { get; set; }
        public string followOnCall { get; set; }
        public string expiryPeriod { get; set; }
        public decimal denomination { get; set; }
        public DateTime dateCreated { get; set; }
        public int numberAdded { get; set; }
        public int numberRemaining { get; set; }
        
        public ICollection<ProductPinsModel> Pins { get; set; }

        public string productCode { get; set; }
        public string providerCode { get; set; }

        public ProductModel products { get; set; }

        
    }
}
