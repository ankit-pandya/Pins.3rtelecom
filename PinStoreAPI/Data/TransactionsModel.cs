using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PinStoreAPI.Data
{
    public class TransactionsModel
    {
        [Key]
        public int id { get; set; }
        public int TransactionID { get; set; }
        public int MerchantID { get; set; }
        public string TerminalId { get; set; }
        public string Type { get; set; }
        public DateTime DateandTime { get; set; }
        public string TxnNumber { get; set; }
        public string ProviderCode { get; set; }
        public string ProviderName { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public decimal Value { get; set; }
        public string ResponseCode { get; set; }
        public string status { get; set; }
        public string SerialNumber { get; set; }
        public string requestData { get; set; }
        public string responseData { get; set; }
    }
}
