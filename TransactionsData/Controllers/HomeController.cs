using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TransactionsData.Data;
using System.Runtime;
using TransactionsData.Models;

namespace TransactionsData.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ApplicationDbContext Context { get; }
        public List<MerchantModel> ListMerchants;
        public List<MerchantTerminals> ListTerminals;

        private readonly ILogger<HomeController> _logger;
        IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _logger = logger;
            Context = context;
        }

        public IActionResult Index()
        {
            //ViewBag.connectionstring = _configuration["ConnectionStrings:SqlData"];


            //var query =
            //   from tproduct in Context.tblproducts 
            //   join tprovider in Context.tblproviders on tproduct.providerCode equals tprovider.providerCode               
            //   select new { tproduct = tproduct, tprovider = tprovider };

            return View();
        }
        
        public string OpenModelPopup()
        {
            //can send some data also.  
            return "<h1>This is Modal Popup Window</h1>";
        }

        public ActionResult ShowMerchants()
        {
            string viewNametoShow = "_getMerchants";
            return PartialView(viewNametoShow);
        }

        [HttpPost]
        // [AutoValidateAntiforgeryToken]
        public IActionResult Search(MerchantModel merchant)
        {
            if (merchant.MerchantID.ToString() == "" && merchant.Name == "" && merchant.OldMID.ToString() == "" && merchant.TerminalId == ""
                && merchant.Address1 == "" && merchant.ContactName == "" && merchant.Postcode == "")
            {
                ListMerchants = (from m in Context.Merchants select m).ToList();
            }
            else
            {
                string midtosearch = "";
                string oldmidtosearch = "";
                if (merchant.MerchantID == 0)
                {
                    midtosearch = "";
                }
                else
                {
                    midtosearch = merchant.MerchantID.ToString();
                }
                if (merchant.OldMID == 0)
                {
                    oldmidtosearch = "";
                }
                else
                {
                    oldmidtosearch = merchant.OldMID.ToString();
                }


                ListMerchants = (from m in Context.Merchants select m).ToList();
                ListTerminals = (from m in Context.MerchantTerminal select m).ToList();

                if (midtosearch != "")
                {
                    try
                    {
                        ListMerchants = ListMerchants.Where(m => m.MerchantID.ToString().Contains(midtosearch)).ToList();
                    }
                    catch
                    {

                    }
                    
                }
                if (oldmidtosearch != "")
                {
                    try
                    {
                        ListMerchants = ListMerchants.Where(m => m.OldMID.ToString().Contains(oldmidtosearch)).ToList();
                    }
                    catch { }
                    
                }
                if (merchant.Name != null && merchant.Name != "")
                {
                    try { ListMerchants = ListMerchants.Where(m => m.Name.ToString().ToUpper().Contains(merchant.Name.ToUpper())).ToList(); } catch { }
                }
                if (merchant.TerminalId != null && merchant.TerminalId != "")
                {
                    try 
                    {   
                        ListMerchants.Clear();
                        ListTerminals = ListTerminals.Where(m => m.TerminalId.ToString().ToUpper().Contains(merchant.TerminalId.ToUpper())).ToList(); 
                        if(ListTerminals != null)
                        {
                            foreach (var mt in ListTerminals)
                            {
                                MerchantModel ListofMerchants = (from m in Context.Merchants where m.MerchantID == mt.MerchantID select m).FirstOrDefault();
                                ListMerchants.Add(ListofMerchants);
                            }
                        }
                    } 
                    catch { }
                }
                if (merchant.Address1 != null && merchant.Address1 != "")
                {
                    try
                    {
                        ListMerchants = ListMerchants.Where(m => m.Address1.ToString().ToUpper().Contains(merchant.Address1.ToUpper()) ||
                                                                m.Address3.ToUpper().Contains(merchant.Address1.ToUpper()) ||
                                                                m.Address4.ToUpper().Contains(merchant.Address1.ToUpper()) ||
                                                                m.Postcode.ToUpper().Contains(merchant.Address1.ToUpper())).ToList();
                    }
                    catch
                    {

                    }
                }
                if (merchant.ContactName != null && merchant.ContactName != "")
                {
                    try { ListMerchants = ListMerchants.Where(m => m.ContactName.ToString().ToUpper().Contains(merchant.ContactName.ToUpper())).ToList(); } catch { }
                }
                if (merchant.Postcode != null && merchant.Postcode != "")
                {
                    try { ListMerchants = ListMerchants.Where(m => m.Postcode.ToString().ToUpper().Contains(merchant.Postcode.ToUpper())).ToList(); } catch { }
                }



            }

            return Json(new { data = ListMerchants });
            //string viewNametoShow = "_getMerchantList";
            //return PartialView(viewNametoShow, Json(new { data = ListMerchants }));
        }

        public ActionResult SearchMerchant(string mid)
        {
            int numId = 0;
            if (mid == null || mid == "")
                return BadRequest();

            if (int.TryParse(mid, out int num))
                numId = num;

            var mData = Context.Merchants.Where(m => m.MerchantID == numId).ToList();


            return Json(new { data = mData });
        }

        public ActionResult Searched()
        {
            return Json(new { data = ListMerchants });
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Search1()
        {

            return Json(new { data = "" });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
