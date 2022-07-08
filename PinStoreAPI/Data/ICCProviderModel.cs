using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinStoreAPI.Data
{
    public class ICCProviderModel
    {
        [Key]
        public int id { get; set; }
        public string SupplierID { get; set; }
        public string SupplierName { get; set; }        
        public string Status { get; set; }
    }
}
