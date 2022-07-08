using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PinStoreAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PinStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeeklyTransactionsController : Controller
    {
        public WeeklyTransactionsController(ApplicationDbContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }

        public ApplicationDbContext Context { get; }
        public IConfiguration Configuration { get; }

        [HttpGet]
        public async Task<string> IndexAsync(string from, string to, string type)
        {
            Log lg = new Log();
            try
            {
                CreateWeeklyReport report = new CreateWeeklyReport(Context, Configuration);                
                var filetosend = await report.ExportInvoicesAsync(from, to, type);
                
                string typeofTransactions = "MTU";
                if (type == "ICC")
                {
                    typeofTransactions = "ICC";
                }

                string body = $"({typeofTransactions}) Weekly transactions data from {from} to {to}. \n" +
                                      $"\n\n\n Thank you, \n 3R Pinstore";

                SendEmail se = new SendEmail();                
                se.SendEmailNow($"({typeofTransactions}) list of transactions from {from} to {to}", body, filetosend);
            }
            catch (Exception e)
            {                
                lg.Logt("Send Transaction report Error: " + e.Message +"\n" + e.StackTrace);
            }
            
            return "Email Sent";
        }

        
    }
}
