using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinStoreAPI.Data
{
    public class MerchantTerminals
    {
        [Key]
        public int id { get; set; }
        public int MerchantID { get; set; }
        public string TerminalId { get; set; }
        public string MacId { get; set; }
        public string IpAddress { get; set; }
        public string DiD { get; set; }
        public string Origin { get; set; }
        public string TerminalRef { get; set; }
    }
}
