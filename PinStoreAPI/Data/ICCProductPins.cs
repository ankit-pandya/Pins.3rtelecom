using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PinStoreAPI.Data
{
    
    public class ICCProductPins
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]       
        public int id { get; set; }
        public string Pin{ get; set; }
        public string  Serial { get; set; }
        public int ICCstkid { get; set; }
        public DateTime DateSold { get; set; }
        public string TransactionId { get; set; }

        public ICCProductStockLevels ICCStk { get; set; }
    }
}
