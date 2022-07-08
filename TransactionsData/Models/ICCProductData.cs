using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionsData.Models
{
    public class ICCProductData
    {        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string ProviderCode { get; set; }
        public string ProductCode { get; set; }        
        public string FreephoneNumber { get; set; }
        public string GeneralNumber { get; set; }
        public string LondonNumber { get; set; }        
        public string HelplineNumber { get; set; }
        public string FollowOnCall { get; set; }
        public string ExpiryPeriod { get; set; }
        public string Pin { get; set; }
        public string Serial { get; set; }
        public string Value { get; set; }
        public DateTime DateAdded { get; set; }

       // public ICollection<ICCProductPins> pins { get; set; }

    }
}
