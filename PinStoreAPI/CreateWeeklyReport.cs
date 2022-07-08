using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PinStoreAPI.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinStoreAPI
{
    public class CreateWeeklyReport
    {
        public CreateWeeklyReport(ApplicationDbContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }

        public ApplicationDbContext Context { get; }
        public IConfiguration Configuration { get; }

        public async Task<string> ExportInvoicesAsync(string from, string to, string type)
        {
            var parsedDatefrom = DateTime.Parse(from);
            var parsedDateto = DateTime.Parse(to).Add(new TimeSpan(23, 59, 59));
            var tData = await Context.Transactions.Where(T => T.ResponseCode == "").ToListAsync();
            if (type == "ICC")
            {
                tData = await Context.Transactions.Where(T => T.DateandTime >= parsedDatefrom && T.DateandTime <= parsedDateto && T.SerialNumber != null && T.ResponseCode == "00").ToListAsync();
            }
            else
            {
                tData = await Context.Transactions.Where(T => T.DateandTime >= parsedDatefrom && T.DateandTime <= parsedDateto && T.SerialNumber == null && T.ResponseCode == "00").ToListAsync();
            }

            StringBuilder sb = new StringBuilder();
            if (tData != null)
            {
                if (type != "ICC")
                {
                    sb.Append("TransactionID,TerminalID,SiteID,TypeID,Type,CardDetails,Form,Network,Type,Value,Type,Code,Country,TXNStatus, , ,SiteID,TXNDate,TXNTime\r\n");
                    foreach (var item in tData)
                    {
                        sb.Append($"{item.TransactionID},{item.TerminalId},{item.MerchantID},,Voucher,{item.ProductCode},Swiped,{item.ProviderName},EV,{item.Value / 100},Voucher,826,GBR,Settled, , ,{item.MerchantID},{item.DateandTime.ToShortDateString()},{item.DateandTime.ToString().Substring(12)}\r\n");
                    }
                }
                else
                {
                    sb.Append("\r\n");
                    sb.Append("------------------------------------------------------------------------------------------------------------------------------------------\r\n");
                    foreach (var item in tData)
                    {
                        sb.Append($"{string.Concat(Enumerable.Repeat(" ", 34))}{item.Value / 100}{item.TransactionID}{string.Concat(Enumerable.Repeat(" ", 23))}{item.MerchantID}{string.Concat(Enumerable.Repeat(" ", 9))}{item.DateandTime}{item.TerminalId}S{string.Concat(Enumerable.Repeat(" ", 4))}{item.ResponseCode}" +
                            $"{string.Concat(Enumerable.Repeat(" ", 54))}{item.ProductCode}{string.Concat(Enumerable.Repeat(" ", 8))}{item.SerialNumber.Replace((char)13, ' ').PadRight(32)}{item.ProviderCode}\r\n");
                    }
                }
            }

            //var contenet = Encoding.UTF8.GetBytes(sb.ToString());
            string filename = "";

            if (type == "ICC")
            {
                filename = $"Pinstore Transactions.prn";
            }
            else
            {
                filename = $"MTU Transactions.csv";
            }

            string filepath = $"Reports/{filename}";
            
            File.WriteAllText(filepath, sb.ToString());

            return filepath;
        }
    }
}
