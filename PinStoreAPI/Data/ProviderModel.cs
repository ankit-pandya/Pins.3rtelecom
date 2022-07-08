using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinStoreAPI.Data
{
    public class ProviderModel
    {
        [Key]
        public int id { get; set; }
        public string providerCode { get; set; }
        public string providerName { get; set; }
        public string status { get; set; }
    }
}
