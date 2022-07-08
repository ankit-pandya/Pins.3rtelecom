using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        // ----------------------------------------------------- Providers ---------------------------------------------------------------------------------
        public IActionResult Providers()
        {
            return View();
        }

        public IActionResult NewProvider()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateProvider(ProviderModel provider)
        {
            if (provider.providerCode != null && provider.providerName!= null && provider.status != null)
            {
                _context.tblproviders.Add(provider);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Providers));
        }

        public async Task<IActionResult> ShowProvider(string providerID)
        {
            var DataRecord = await _context.tblproviders.FirstOrDefaultAsync(m => m.providerCode == providerID);

            if (DataRecord == null)
                return NotFound();

            return View("ShowProvider", DataRecord);
        }

        [HttpPost]
        public IActionResult Searchprovider(ProviderModel provider)
        {
            List<ProviderModel> ListProviders;

            if (provider.providerCode == "" && provider.providerName == "")
            {
                ListProviders = (from m in _context.tblproviders select m).ToList();
            }
            else
            {
                ListProviders = (from m in _context.tblproviders select m).ToList();
                if (provider.providerName != null && provider.providerName != "") ListProviders = ListProviders.Where(m => m.providerName.ToString().ToUpper().Contains(provider.providerName.ToUpper())).ToList();
                if (provider.providerCode != null && provider.providerCode != "") ListProviders = ListProviders.Where(m => m.providerCode.ToString().ToUpper().Contains(provider.providerCode.ToUpper())).ToList();
            }
            return Json(new { data = ListProviders });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateProvider(ProviderModel provider)
        {
            _context.tblproviders.Update(provider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Providers));
        }
        // ----------------------------------------------------------------------------------------------------------------------------------------------
        // ----------------------------------------------------- Products ---------------------------------------------------------------------------------
        public IActionResult Products()
        {
            return View();
        }

        public IActionResult NewProduct()
        {            
            var data  = (from tprd in _context.tblproviders select new { Text = tprd.providerCode + " - " + tprd.providerName, Value = tprd.providerCode }).ToList();
            
            List<SelectListItem> statues = new List<SelectListItem>();            

            foreach (var item in data)
            {
                statues.Add(new SelectListItem(){ Text = item.Text, Value = item.Value});                
            }

            ViewBag.Options = statues;
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateProduct(ProductModel product)
        {
            if (product.productCode != null && product.productName != null && product.status != null)
            {
                _context.tblproducts.Add(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Products));
        }

        public async Task<IActionResult> ShowProduct(string productid)
        {
            var DataRecord = await _context.tblproducts.FirstOrDefaultAsync(m => m.productCode == productid);

            if (DataRecord == null)
                return NotFound();

            var data = (from tprd in _context.tblproviders select new { Text = tprd.providerCode + " - " + tprd.providerName, Value = tprd.providerCode }).ToList();

            List<SelectListItem> statues = new List<SelectListItem>();

            foreach (var item in data)
            {
                statues.Add(new SelectListItem() { Text = item.Text, Value = item.Value });
            }

            ViewBag.Options = statues;

            var DataProviderRecord = await _context.tblproviders.FirstOrDefaultAsync(m => m.providerCode == DataRecord.providerCode);
            ViewBag.SelectedOptions = DataProviderRecord.providerCode + " - " + DataProviderRecord.providerName;

            return View("ShowProduct", DataRecord);
        }

        [HttpPost]
        public IActionResult Searchproduct(ProductModel product)
        {
            List<ProductModel> ListProviders;

            if (product.productCode == "" && product.productName == "")
            {
                ListProviders = (from m in _context.tblproducts select m).ToList();
            }
            else
            {
                ListProviders = (from m in _context.tblproducts select m).ToList();
                if (product.productName != null && product.productName != "") ListProviders = ListProviders.Where(m => m.productName.ToString().ToUpper().Contains(product.productName.ToUpper())).ToList();
                if (product.productCode != null && product.productCode != "") ListProviders = ListProviders.Where(m => m.productCode.ToString().ToUpper().Contains(product.productCode.ToUpper())).ToList();
            }
            return Json(new { data = ListProviders });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateProduct(ProductModel product)
        {
            _context.tblproducts.Update(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Products));
        }
        // ----------------------------------------------------------------------------------------------------------------------------------------------

        // -------------------------------------- Pins ------------------------------------------------------------------------------------------------

        public IActionResult Pins()
        {
            return View();
        }

        List<ProductDenominations> ListPins;
        List<ProductModel> ListProducts;

        [HttpPost]
        public IActionResult SearchPin(string prodcode, string prodName)
        {
            if (prodcode == null) prodcode = "";
            if (prodName == null) prodName = "";

            if (prodcode == "" && prodName == "")
            {
                ListPins = (from m in _context.tblproductdenoms select m).ToList();
            }

            if (prodName != "" && prodcode == "")
            {
                ListProducts = (from st in _context.tblproducts where st.productName.ToUpper().Contains(prodName.ToUpper()) select st).ToList();

                foreach (var prd in ListProducts)
                {
                    var data = (from ps in _context.tblproductdenoms where ps.productCode == prd.productCode select ps).ToList();
                    if (data.Count > 0)
                    {
                        foreach (var item in data)
                        {
                            ListPins.Add(item);
                        }
                    }
                }
            }

            if (prodName == "" && prodcode != "")
            {
                ListPins = (from m in _context.tblproductdenoms select m).ToList();
                if (prodcode != null && prodcode != "") ListPins = ListPins.Where(m => m.productCode.ToString().ToUpper().Contains(prodcode.ToUpper())).ToList();
            }

            if (prodName != "" && prodcode != "")
            {
                ListProducts = (from st in _context.tblproducts where st.productName.ToUpper().Contains(prodName.ToUpper()) select st).ToList();

                foreach (var prd in ListProducts)
                {
                    var data = (from ps in _context.tblproductdenoms where ps.productCode == prd.productCode select ps).ToList();
                    if (data.Count > 0)
                    {
                       foreach (var item in data)
                        {
                            ListPins.Add(item);
                        }
                    }
                }

                ListPins = ListPins.Where(m => m.productCode.ToString().ToUpper().Contains(prodcode.ToUpper())).ToList();

            }

            var PinsData = ListPins.GroupBy(l => l.productCode).Select(cl => new Models.ProductDenominations
            {
                productCode = cl.Select(x => x.productCode).FirstOrDefault(),
                providerCode = cl.Select(x => x.providerCode).FirstOrDefault(),
                denomination = cl.Select(x => x.denomination).FirstOrDefault(),
                numberRemaining = cl.Sum(c => c.numberRemaining),
                followOnCall = ""
            }).ToList();

            foreach (var item in PinsData)
            {
                var dataforProdName = (from st in _context.tblproducts where st.providerCode.ToUpper().Contains(item.providerCode.ToUpper()) && st.productCode.ToUpper().Contains(item.productCode.ToUpper()) select st).FirstOrDefault();
                
                if (dataforProdName != null)
                    item.followOnCall = dataforProdName.productName;
            }

            return Json(new { data = PinsData });
        }

        [HttpPost]
        public async Task<ActionResult> PinUpload(IFormFile postedFile)
        {
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


                if (filePath.Contains("merchants"))
                {

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await postedFile.CopyToAsync(stream);
                    }

                    //Read the contents of CSV file.
                    string csvDatamerchants = System.IO.File.ReadAllText(filePath);


                    try
                    {

                        //Execute a loop over the rows.
                        foreach (string row in csvDatamerchants.Split('\n'))
                        {
                            if (!string.IsNullOrEmpty(row))
                            {
                                string[] words = row.Split(',');

                                if (words[0] == "")
                                    continue;

                                MerchantModel merchants = new MerchantModel();

                                merchants.MerchantID = int.Parse(words[0]);
                                merchants.OldMID = int.Parse(words[1]);
                                merchants.Name = words[2];
                                merchants.ContactName = words[3];
                                merchants.Address1 = words[4];
                                merchants.Address3 = words[5];
                                merchants.Address4 = words[6];
                                merchants.Postcode = words[7];
                                merchants.Status = "ENABLED";
                                merchants.Balance = 1500;
                                merchants.CreditLimit = 3500;
                                merchants.BalWarning = 300;
                                merchants.Type = "CREDIT";
                                merchants.PinMode = "Online First";
                                merchants.Telephone = words[8];


                                _context.Merchants.Add(merchants);
                            }
                        }

                        await _context.SaveChangesAsync();

                    }
                    catch (Exception e)
                    {
                        var dd = e.StackTrace;
                        var ee = e.InnerException;
                    }

                    return View();
                }


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await postedFile.CopyToAsync(stream);
                }

                //Read the contents of CSV file.
                string csvData = System.IO.File.ReadAllText(filePath);


                try
                {
                    //ICCProductStockLevels IccStockLevel = new ICCProductStockLevels();
                    ProductDenominations prdDenoms = new ProductDenominations();
                    int totalPins = 0;
                    List<ProductPinsModel> Allpins = new();

                    //Execute a loop over the rows.
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            string[] words = row.Split(',');

                            if (words[0] == "")
                                continue;

                            prdDenoms.dateCreated = DateTime.Now;

                            prdDenoms.providerCode = words[0];
                            prdDenoms.productCode = words[1];
                            prdDenoms.freephoneNumber = words[2];
                            prdDenoms.generalNumber = words[3];
                            prdDenoms.londonNumber = words[4];
                            prdDenoms.helplineNumber = words[5];
                            prdDenoms.followOnCall = words[5];
                            prdDenoms.expiryPeriod = words[6];
                            prdDenoms.denomination = decimal.Parse(words[7]);

                            ProductPinsModel pintoload = new ProductPinsModel();
                            pintoload.pin = words[8];
                            pintoload.serial = words[9];
                            totalPins += 1;

                            Allpins.Add(pintoload);

                            pintoload.productDenominations = prdDenoms;
                            _context.tblproductpins.Add(pintoload);
                        }
                    }

                    prdDenoms.numberAdded = totalPins;
                    prdDenoms.numberRemaining = totalPins;
                    prdDenoms.Pins = Allpins;

                    //_context.ICCProductStockLevels.Add(IccStockLevel);
                    _context.tblproductdenoms.Add(prdDenoms);

                    await _context.SaveChangesAsync();

                }
                catch (Exception e)
                {
                    var dd = e.StackTrace;
                    var ee = e.InnerException;
                }
            }


            return RedirectToAction(nameof(Pins));
        }
        // ----------------------------------------------------------------------------------------------------------------------------------------------
    }
}
