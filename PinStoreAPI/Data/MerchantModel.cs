using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinStoreAPI.Data
{
    public class MerchantModel
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Merchant ID")]
        public int MerchantID { get; set; }

        [Display(Name = "Old Merchant ID")]
        public int OldMID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        public string Address1 { get; set; }

        [Display(Name = "Line 2")]
        public string Address2 { get; set; }

        [Display(Name = "Town")]
        public string Address3 { get; set; }

        [Display(Name = "County")]
        public string Address4 { get; set; }

        [Display(Name = "Postcode")]
        public string Postcode { get; set; }

        [Display(Name = "Terminal ID")]
        public string TerminalId { get; set; }

        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }
        public string Status { get; set; }
        public decimal Balance { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal BalWarning { get; set; }
        public string Type { get; set; }
        public string PinMode { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }

        public bool allowEE { get; set; }
        public bool emailbulk { get; set; }

        public string MonOH { get; set; }
        public string MonOM { get; set; }
        public string MonCH { get; set; }
        public string MonCM { get; set; }

        public string TueOH { get; set; }
        public string TueOM { get; set; }
        public string TueCH { get; set; }
        public string TueCM { get; set; }

        public string WedOH { get; set; }
        public string WedOM { get; set; }
        public string WedCH { get; set; }
        public string WedCM { get; set; }

        public string ThuOH { get; set; }
        public string ThuOM { get; set; }
        public string ThuCH { get; set; }
        public string ThuCM { get; set; }

        public string FriOH { get; set; }
        public string FriOM { get; set; }
        public string FriCH { get; set; }
        public string FriCM { get; set; }

        public string SatOH { get; set; }
        public string SatOM { get; set; }
        public string SatCH { get; set; }
        public string SatCM { get; set; }

        public string SunOH { get; set; }
        public string SunOM { get; set; }
        public string SunCH { get; set; }
        public string SunCM { get; set; }
    }
}
