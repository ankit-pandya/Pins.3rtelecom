using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinStoreAPI.Data
{
    public class MerchantBalance
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Merchant ID")]
        public int MerchantID { get; set; }

        public decimal Balance { get; set; }
    }
}
