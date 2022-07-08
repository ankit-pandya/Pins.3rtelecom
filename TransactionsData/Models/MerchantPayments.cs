using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionsData.Models
{
    public class MerchantPayments
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public DateTime paydatetime { get; set; }
        public decimal amount { get; set; }
        public decimal balance { get; set; }
        public int mid { get; set; }

        public int merchantID { get; set; }
        public MerchantModel merchant { get; set; }
    }
}
