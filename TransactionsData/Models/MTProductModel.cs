using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionsData.Models
{
    public class MTProductModel
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "3R Product Code")]
        public string productCode3r { get; set; }
        [Display(Name = "Product Code")]
        public string productCode { get; set; }
        [Display(Name = "Product Name")]
        public string productName { get; set; }
        [Display(Name = "Status")]
        public string status { get; set; }
        [Display(Name = "Value")]
        public int value { get; set; }
        [Display(Name = "Information")]
        public string information { get; set; }
        [Display(Name = "3R Provider")]
        public string provider3r { get; set; }
        [Display(Name = "3R Product")]
        public string product3r { get; set; }
        
    }
}
