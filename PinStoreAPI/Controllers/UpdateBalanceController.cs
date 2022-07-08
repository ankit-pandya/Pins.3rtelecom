using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PinStoreAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateBalanceController : Controller
    {
        public ApplicationDbContext Context { get; }
        public IConfiguration Configuration { get; }

        public UpdateBalanceController(ApplicationDbContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }

        [HttpGet]
        public string Index()
        {
            var merchants = (from m in Context.Merchants.Where(m=> m.Type.ToUpper() == "CREDIT" && m.Status.ToUpper() == "ENABLED") select m).ToList();
            foreach (var merchant in merchants)
            {
                if (merchant.Balance != merchant.CreditLimit)
                {
                    merchant.Balance = merchant.CreditLimit;
                    Context.Merchants.Update(merchant);
                }

                if (merchant.MonOH == null)
                    merchant.MonOH = "00"; merchant.MonOM = "00"; merchant.MonCH = "23"; merchant.MonCM = "59"; Context.Merchants.Update(merchant);

                if (merchant.TueOH == null)
                    merchant.TueOH = "00"; merchant.TueOM = "00"; merchant.TueCH = "23"; merchant.TueCM = "59"; Context.Merchants.Update(merchant);

                if (merchant.WedOH == null)
                    merchant.WedOH = "00"; merchant.WedOM = "00"; merchant.WedCH = "23"; merchant.WedCM = "59"; Context.Merchants.Update(merchant);

                if (merchant.ThuOH == null)
                    merchant.ThuOH = "00"; merchant.ThuOM = "00"; merchant.ThuCH = "23"; merchant.ThuCM = "59"; Context.Merchants.Update(merchant);

                if (merchant.FriOH == null)
                    merchant.FriOH = "00"; merchant.FriOM = "00"; merchant.FriCH = "23"; merchant.FriCM = "59"; Context.Merchants.Update(merchant);

                if (merchant.SatOH == null)
                    merchant.SatOH = "00"; merchant.SatOM = "00"; merchant.SatCH = "23"; merchant.SatCM = "59"; Context.Merchants.Update(merchant);

                if (merchant.SunOH == null)
                    merchant.SunOH = "00"; merchant.SunOM = "00"; merchant.SunCH = "23"; merchant.SunCM = "59"; Context.Merchants.Update(merchant);

                var mBalance = (from m in Context.tblMerchantBalance.Where(mb => mb.MerchantID == merchant.MerchantID) select m).FirstOrDefault();

                if (mBalance != null)
                {
                    if(mBalance.Balance != merchant.CreditLimit)
                    {
                        mBalance.Balance = merchant.CreditLimit;
                        Context.tblMerchantBalance.Update(mBalance);
                    }
                }
                else
                {
                    MerchantBalance newMerchantBalance = new MerchantBalance();
                    newMerchantBalance.MerchantID = merchant.MerchantID;
                    newMerchantBalance.Balance = merchant.Balance;
                    Context.tblMerchantBalance.Add(newMerchantBalance);
                }
            }

            Context.SaveChanges(); ;

            return "Udpated!";
        }
    }
}
