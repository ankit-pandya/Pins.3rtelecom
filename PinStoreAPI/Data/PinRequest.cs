using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinStoreAPI.Data
{
    public class PinRequest
    {
        public int mid { get; set; }
        public string tid { get; set; }
        public string txnNO { get; set; }
        public string supplierCode { get; set; }
        public string productCode { get; set; }
        public decimal value { get; set; }
    }
}
