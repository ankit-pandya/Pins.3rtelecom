using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TransactionsData.Data;
using TransactionsData.Models;

namespace TransactionsData.Controllers
{
    [Authorize]
    public class MerchantController : Controller
    {
        public ApplicationDbContext _context { get; }

        public MerchantController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string mid)
        {
            int numId = 0;
            if (mid == null || mid == "")
                return BadRequest();

            if (int.TryParse(mid, out int num))
                numId = num;

            var DataRecord = await _context.Merchants.FirstOrDefaultAsync(m => m.MerchantID == numId);

            if (DataRecord == null)
                return NotFound();

            return View(DataRecord);
            
        }

        [HttpPost]
        public async Task<string> UploadPayment(string mid, string amt)
        {   
            decimal amount = decimal.Parse(amt);
            decimal newBalance = amount;

            
            MerchantBalance mbalance = await (from m in _context.tblMerchantBalance where m.MerchantID == int.Parse(mid) select m).FirstOrDefaultAsync();
            MerchantModel mb = await (from m in _context.Merchants where m.MerchantID == int.Parse(mid) select m).FirstOrDefaultAsync();

            if (mbalance == null)
            {
                mbalance = new MerchantBalance();
                mbalance.MerchantID = int.Parse(mid);
                mbalance.Balance = 0;

                _context.tblMerchantBalance.Add(mbalance);
                _context.SaveChanges();
            }

            mbalance = await (from m in _context.tblMerchantBalance where m.MerchantID == int.Parse(mid) select m).FirstOrDefaultAsync();

            decimal amount2 = decimal.Parse(mbalance.Balance.ToString());
            newBalance += amount2;

            MerchantPayments mp = new MerchantPayments();
            mp.username = User.Identity.Name; //"user";
            mp.paydatetime = DateTime.Now;
            mp.mid = int.Parse(mid);
            mp.merchant = mb;
            mp.amount = amount;
            mp.balance =  newBalance;

            mb.Balance = newBalance;
            mbalance.Balance = newBalance;

            _context.MerchantPayments.Add(mp);
            _context.tblMerchantBalance.Update(mbalance);
            _context.Merchants.Update(mb);

            await _context.SaveChangesAsync();            

            // string redURL = $"~/Merchant/ShowMerchant?mid={mid}&udpate=false&create=false" ;
            // return LocalRedirect(redURL);
            return newBalance.ToString();

            
        }

        public IActionResult UploadCredits()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> UploadCredits(IFormFile postedFile)
        {
            int totalCredits = 0;
            string filePath = string.Empty;
            if (postedFile != null)
            {
                string path = "~/Uploads/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(postedFile.FileName);
                string extension = Path.GetExtension(postedFile.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await postedFile.CopyToAsync(stream);
                }
                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);

                try
                {
                    //Execute a loop over the rows.
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            string[] rows = row.Split(',');

                            if (rows[0] == "")
                                continue;
                            
                            
                            var md = await (from m in _context.Merchants where m.MerchantID == int.Parse(rows[0]) select m).FirstOrDefaultAsync();
                            var mBalance = await (from m in _context.tblMerchantBalance where m.MerchantID == int.Parse(rows[0]) select m).FirstOrDefaultAsync();
                            mBalance.Balance = int.Parse(rows[2]);

                            md.CreditLimit = int.Parse(rows[1]);
                            md.Balance = int.Parse(rows[2]);
                            totalCredits += 1;

                            _context.tblMerchantBalance.Update(mBalance);
                            _context.Merchants.Update(md);                            
                        }
                    }
                    
                    await _context.SaveChangesAsync();

                }
                catch (Exception e)
                {
                    var dd = e.StackTrace;
                    var ee = e.InnerException;
                }
            }

            return (totalCredits).ToString();
            //return LocalRedirect($"~/Home/Index");
        }

        public async Task<IActionResult> ShowMerchant(string mid, string update, string create)
        {         
            if (update == "true")
            {
                ViewBag.Message = "Merchant Updated!";
            }
            
            if (update == "false" && create == "false")
            {
                ViewBag.Message = null;
            }

            if (create == "true")
            {
                ViewBag.Message = "Merchant Created!";
            }

            update = "false";
            create = "false";

            int numId = 0;
            if (mid == null || mid == "")
                return BadRequest();

            if (int.TryParse(mid, out int num))
                numId = num;

            var DataRecord = await _context.Merchants.FirstOrDefaultAsync(m => m.MerchantID == numId);            

            if (DataRecord == null)
                return NotFound();

            var DataBalacne = await _context.tblMerchantBalance.FirstOrDefaultAsync(m => m.MerchantID == numId);

            if (DataBalacne != null)
            {
                DataRecord.Balance = DataBalacne.Balance;
            }
            else
            {
                DataRecord.Balance = 0;
            }
            return View(DataRecord);

        }

        public IActionResult CreateMerchant()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMerchant(MerchantModel merchant)
        {
            _context.Merchants.Add(merchant);
            await _context.SaveChangesAsync();
            
            //return RedirectToAction(nameof(Index));
            return LocalRedirect($"~/Merchant/ShowMerchant?mid={merchant.MerchantID.ToString()}&udpate=false&create=true");
        }

        [HttpPost]
        public async Task<IActionResult> CreateTerminal(MerchantTerminals terminal)
        {
            _context.MerchantTerminal.Add(terminal);
            await _context.SaveChangesAsync();

            //return RedirectToAction(nameof(Index));
            return LocalRedirect($"~/Merchant/ShowMerchant?mid={terminal.MerchantID.ToString()}");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTerminal(MerchantTerminals terminal)
        {
            _context.MerchantTerminal.Update(terminal);
            await _context.SaveChangesAsync();

            //return RedirectToAction(nameof(Index));
            return LocalRedirect($"~/Merchant/ShowMerchant?mid={terminal.MerchantID.ToString()}");
        }

        [HttpPost]
        public IActionResult UpdateMerchant(MerchantModel merchant)
        {            
            var idtoUpdate = merchant.Id;
            var DataBalacne = _context.tblMerchantBalance.FirstOrDefault(m => m.MerchantID == merchant.MerchantID);
            
            if(DataBalacne != null)
            {
                merchant.Balance = DataBalacne.Balance;
            }
                            
            _context.Merchants.Update(merchant);
            _context.SaveChanges();           

            return LocalRedirect($"~/Merchant/ShowMerchant?mid={merchant.MerchantID.ToString()}&update=true&create=false");
        }

        public ActionResult getAllData()
        {
            return View();
        }

        public async Task<ActionResult> getAll()
        {
            var merchants = await _context.Merchants.ToListAsync();
            return Json(new { data = merchants });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Search(MerchantModel merchant)
        {
            var MerchantList = (from m in _context.Merchants select m).ToList();
            if (merchant.MerchantID.ToString() != null || merchant.Name != null || merchant.OldMID.ToString() != null ||
                merchant.TerminalId != null || merchant.Address1 != null || merchant.ContactName != null || merchant.Postcode != null)
            {                             
                if (merchant.MerchantID.ToString() != null) MerchantList = MerchantList.Where(m => m.MerchantID.ToString().ToUpper().Contains(merchant.MerchantID.ToString().ToUpper())).ToList();
                if (merchant.Name != null) MerchantList= MerchantList.Where(m => m.Name.ToUpper().Contains(merchant.Name.ToUpper())).ToList();
                if (merchant.OldMID.ToString() != null) MerchantList = MerchantList.Where(m => m.OldMID.ToString().ToUpper().Contains(merchant.OldMID.ToString().ToUpper())).ToList();
                if (merchant.TerminalId != null) MerchantList = MerchantList.Where(m => m.TerminalId.ToUpper().Contains(merchant.TerminalId.ToUpper())).ToList();
                if (merchant.Address1 != null) MerchantList = MerchantList.Where(m => m.Address1.ToUpper().Contains(merchant.Address1.ToUpper()) ||
                                                                        m.Address2.ToUpper().Contains(merchant.Address1.ToUpper()) ||
                                                                        m.Address3.ToUpper().Contains(merchant.Address1.ToUpper()) ||
                                                                        m.Address4.ToUpper().Contains(merchant.Address1.ToUpper()) ||
                                                                        m.Postcode.ToUpper().Contains(merchant.Address1.ToUpper())).ToList();
                if (merchant.ContactName != null) MerchantList = MerchantList.Where(m => m.ContactName.ToUpper().Contains(merchant.ContactName.ToUpper())).ToList();
                if (merchant.Postcode != null) MerchantList = MerchantList.Where(m => m.Postcode.ToUpper().Contains(merchant.Postcode.ToUpper())).ToList();
            }

            return View(MerchantList);
        }

        public ActionResult SearchMerchant(string mid)
        {
            int numId = 0;
            if (mid == null || mid == "")
                return BadRequest();

            if (int.TryParse(mid, out int num))
                numId = num;

            var mData = _context.Merchants.Where(m => m.MerchantID == numId).ToList();


            return Json(new { data = mData });
        }
        
        public ActionResult ShowICCTransactions(string mid)
        {
            string viewNametoShow = "_getICCTransactions";
            ViewData["MerchantID"] = mid;            
            return PartialView(viewNametoShow);
        }

        public ActionResult MerchantTerminals(string mid)
        {
            string viewNametoShow = "_getMerchantTerminals";
            ViewData["MerchantID"] = mid;
            return PartialView(viewNametoShow);
        }
        
        public ActionResult ShowMerchantHours(string mid)
        {
            string viewNametoShow = "_getMerchantHours";
            ViewData["MerchantID"] = mid;            
            return PartialView(viewNametoShow);
        }
        

        public ActionResult MerchantPayments(string mid)
        {
            string viewNametoShow = "_getMerchantPayments";
            ViewData["MerchantID"] = mid;
            return PartialView(viewNametoShow);
        }
        public ActionResult getListofPayments(string mid)
        {
            var tData = _context.MerchantPayments.Where(p => p.mid== int.Parse(mid)).OrderByDescending(m=> m.paydatetime).ToList();
            return Json(new { data = tData });
        }

        public async Task<ActionResult> getICCTransactionsAsync(string mid, string from, string to)
        {
            //int numId = 0;
            //if (mid == null || mid == "")
            //    return BadRequest();

            //if (int.TryParse(mid, out int num))
            //    numId = num;

            var parsedDatefrom = DateTime.Parse(from);
            var parsedDateto = DateTime.Parse(to).Add(new TimeSpan(23, 59, 59));
            
            var tData = await _context.Transactions.Where(T => T.MerchantID == int.Parse(mid) && T.DateandTime >= parsedDatefrom && T.DateandTime <= parsedDateto).ToListAsync();
            

            //var mData = Context.Merchants.Where(m => m.MerchantID == numId).ToList();
            int totalsucesstrans = 0;
            decimal totalvalue = 0;            
            
            foreach (var t in tData)
            {
                if (t.ResponseCode == "00")
                {
                    totalsucesstrans += 1;
                    totalvalue += (t.Value / 100); 
                }
            }

            return Json(new { data = tData, tt=tData.Count, tst = totalsucesstrans, tv = $"£{totalvalue}" });
        }
              

        public ActionResult getListofTerminals(string mid)
        {
            var tData = _context.MerchantTerminal.Where(T => T.MerchantID == int.Parse(mid)).ToList();
            return Json(new { data = tData });
        }

        public ActionResult AddTerminal(string mid)
        {
            MerchantTerminals mt = new MerchantTerminals();
            mt.MerchantID = int.Parse(mid);
            return View("NewTerminal", mt);
        }

        public ActionResult ShowTerminal(string tid, string mid)
        {
            MerchantTerminals mt  = (from t in _context.MerchantTerminal where t.TerminalId == tid && t.MerchantID == int.Parse(mid) select t).FirstOrDefault();
            
            return View("ShowMerchantTerminal", mt);
        }

        public ActionResult DeleteTerminal(string tid, string mid)
        {
            ViewData["TIDDELETED"] = "";
            MerchantTerminals mtd = (from t in _context.MerchantTerminal where t.TerminalId == tid && t.MerchantID == int.Parse(mid) select t).FirstOrDefault();
            if(mtd != null)
            {
                _context.MerchantTerminal.Remove(mtd);
                _context.SaveChangesAsync();
            }
                       
            return LocalRedirect($"~/Merchant/ShowMerchant?mid={mid}&update=true&create=false");
        }

        public ActionResult getMTUTransactions(string mid)
        {
            int numId = 0;
            if (mid == null || mid == "")
                return BadRequest();

            if (int.TryParse(mid, out int num))
                numId = num;

            var mData = _context.Merchants.Where(m => m.MerchantID == numId).ToList();


            return Json(new { data = mData });
        }

        public ActionResult getPayments(string mid)
        {
            int numId = 0;
            if (mid == null || mid == "")
                return BadRequest();

            if (int.TryParse(mid, out int num))
                numId = num;

            var mData = _context.Merchants.Where(m => m.MerchantID == numId).ToList();


            return Json(new { data = mData });
        }

        public ActionResult getTerminals(string mid)
        {
            int numId = 0;
            if (mid == null || mid == "")
                return BadRequest();

            if (int.TryParse(mid, out int num))
                numId = num;

            var mData = _context.Merchants.Where(m => m.MerchantID == numId).ToList();


            return Json(new { data = mData });
        }

        public async Task<ActionResult> ShowLogs(string transid)
        {            
            ViewData["TransactionID"] = transid;
            return PartialView("_getLogs");
            //return Json(new { data = tData });
        }
        public async Task<ActionResult> getLogsData(string tid)
        {
            var tData = _context.Transactions.Where(T => T.TransactionID == int.Parse(tid)).FirstOrDefault();            
            return Json(new { data = tData });
        }

        //public async Task<ActionResult> CheckTransactions()
        //{
        //    var mData = await _context.Merchants.ToListAsync();
        //    foreach (var merchant in mData)
        //    {
        //        var tData = await _context.Transactions.Where(T => T.MerchantID == merchant.MerchantID
        //                                                        && T.DateandTime >= DateTime.Now.AddMinutes(-5) && T.DateandTime <= DateTime.Now).ToListAsync();

        //        if(tData.Count > 9)
        //        {
        //            return RedirectToAction("Index", "SendEmail");                           
        //        }
        //    }
        //    return Redirect("");
        //}
    }
}