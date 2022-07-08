using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionsData.Models
{
    public class ICCProductStockLevels
    {
        [Key]
        public int id { get; set; }
        public string ProviderCode { get; set; }
        public string ProductCode { get; set; }
        public string FreephoneNumber { get; set; }
        public string GeneralNumber { get; set; }
        public string LondonNumber { get; set; }
        public string HelplineNumber { get; set; }
        public string FollowOnCall { get; set; }
        public string ExpiryPeriod { get; set; }
        public string value { get; set; }
        public DateTime dateCreated { get; set; }
        public int numberAdded { get; set; }
        public int numberRemaining { get; set; }

        public ICollection<ICCProductPins> Pins { get; set; }

    }
}
