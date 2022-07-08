using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinStoreAPI.Data
{
    public class MTProductModel
    {
        [Key]
        public int id { get; set; }
        public string productCode3r { get; set; }
        public string productCode { get; set; }
        public string productName { get; set; }
        public string status { get; set; }
        public int value { get; set; }
        public string information { get; set; }
        public string provider3r { get; set; }
        public string product3r { get; set; }
    }
}
