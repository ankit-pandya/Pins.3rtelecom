using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    // [Authorize]
    public class ICCController : Controller
    {
        public List<ICCProviderModel> ListProviders;
        public List<ICCProductModel> ListProducts;
        public List<ICCProductStockLevels> ListPins;

        public ApplicationDbContext Context { get; }

        public ICCController(ApplicationDbContext context)
        {
            Context = context;
        }
        public IActionResult NewProvider()
        {
            return View();
        }

        public IActionResult Provider()
        {
            return View();
        }

        public async Task<IActionResult> ShowProvider(string providerID)
        {
            var DataRecord = await Context.ICCProviders.FirstOrDefaultAsync(m => m.SupplierID == providerID);

            if (DataRecord == null)
                return NotFound();

            return View("ShowProvider", DataRecord);
        }

        [HttpPost]
        public IActionResult Searchprovider(ICCProviderModel provider)
        {
            if (provider.SupplierID == "" && provider.SupplierName == "")
            {
                ListProviders = (from m in Context.ICCProviders select m).ToList();
            }
            else
            {
                ListProviders = (from m in Context.ICCProviders select m).ToList();
                if (provider.SupplierName != null && provider.SupplierName != "") ListProviders = ListProviders.Where(m => m.SupplierName.ToString().ToUpper().Contains(provider.SupplierName.ToUpper())).ToList();
                if (provider.SupplierID != null && provider.SupplierID != "") ListProviders = ListProviders.Where(m => m.SupplierID.ToString().ToUpper().Contains(provider.SupplierID.ToUpper())).ToList();
            }
            return Json(new { data = ListProviders });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateProvider(ICCProviderModel provider)
        {
            if (provider.SupplierID != null && provider.SupplierName != null && provider.Status != null)
            {
                Context.ICCProviders.Add(provider);
                await Context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Provider));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateProvider(ICCProviderModel provider)
        {
            Context.ICCProviders.Update(provider);
            await Context.SaveChangesAsync();

            return RedirectToAction(nameof(Provider));
        }

        //-------------------------------------------------------   PRODUCTS -------------------------------------------------------------------------------------------------------------
        public IActionResult NewProduct()
        {
            return View();
        }

        public IActionResult Product()
        {
            return View();
        }

        public async Task<IActionResult> ShowProduct(string productID)
        {
            var DataRecord = await Context.ICCProducts.FirstOrDefaultAsync(m => m.ProductCode == productID);

            if (DataRecord == null)
                return NotFound();

            return View("ShowProduct", DataRecord);
        }

        [HttpPost]
        public IActionResult Searchproduct(ICCProductModel product)
        {
            if (product.ProductCode == "" && product.ProductName == "")
            {
                ListProducts = (from m in Context.ICCProducts select m).ToList();
            }
            else
            {
                ListProducts = (from m in Context.ICCProducts select m).ToList();
                if (product.ProductName != null && product.ProductName != "") ListProducts = ListProducts.Where(m => m.ProductName.ToString().ToUpper().Contains(product.ProductName.ToUpper())).ToList();
                if (product.ProductCode != null && product.ProductCode != "") ListProducts = ListProducts.Where(m => m.ProductCode.ToString().ToUpper().Contains(product.ProductCode.ToUpper())).ToList();
            }
            return Json(new { data = ListProducts });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateProduct(ICCProductModel product)
        {

            Context.ICCProducts.Add(product);
            await Context.SaveChangesAsync();

            return RedirectToAction(nameof(Product));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> UpdateProduct(ICCProductModel product)
        {
            Context.ICCProducts.Update(product);
            await Context.SaveChangesAsync();

            return RedirectToAction(nameof(product));
        }

        //-------------------------------------------------------   PINS  -------------------------------------------------------------------------------------------------------------
        public IActionResult NewPin()
        {
            return View();
        }

        public IActionResult Pin()
        {
            return View();
        }

        public async Task<IActionResult> ShowPin(string prodID)
        {
            var DataRecords = (from p in Context.ICCProductStockLevels where p.ProductCode.ToUpper().Contains(prodID.ToUpper()) select p).ToList();
            
            if (DataRecords == null)
                return NotFound();

            return View("ShowPin", DataRecords);
        }

        [HttpPost]
        public IActionResult SearchPin(string prodcode, string prodName)
        {
            ListPins = new List<ICCProductStockLevels>();

            if (prodcode == null) prodcode = "";
            if (prodName == null) prodName = "";

            if (prodcode == "" && prodName == "")
            {
                ListPins = (from m in Context.ICCProductStockLevels select m).ToList();
            }            

            if (prodName != "" && prodcode == "")
            {
                ListProducts = (from st in Context.ICCProducts where st.ProductName.ToUpper().Contains(prodName.ToUpper()) select st).ToList();

                foreach (var prd in ListProducts)
                {
                    var data = (from ps in Context.ICCProductStockLevels where ps.ProductCode == prd.ProductCode select ps).ToList();
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
                ListPins = (from m in Context.ICCProductStockLevels select m).ToList();
                if (prodcode != null && prodcode != "") ListPins = ListPins.Where(m => m.ProductCode.ToString().ToUpper().Contains(prodcode.ToUpper())).ToList();
            }

            if (prodName != "" && prodcode != "")
            {
                ListProducts = (from st in Context.ICCProducts where st.ProductName.ToUpper().Contains(prodName.ToUpper()) select st).ToList();

                foreach (var prd in ListProducts)
                {
                    var data = (from ps in Context.ICCProductStockLevels where ps.ProductCode == prd.ProductCode select ps).ToList();
                   if(data.Count > 0)
                    {
                        foreach (var item in data)
                        {
                            ListPins.Add(item);
                        }
                    }                    
                }

                ListPins = ListPins.Where(m => m.ProductCode.ToString().ToUpper().Contains(prodcode.ToUpper())).ToList();

            }

            var PinsData = ListPins.GroupBy(l => l.ProductCode).Select(cl => new Models.ICCProductStockLevels{
                ProductCode = cl.Select(x => x.ProductCode).FirstOrDefault(),
                ProviderCode = cl.Select(x => x.ProviderCode).FirstOrDefault(),
                value = cl.Select(x=> x.value).FirstOrDefault(),                
                numberRemaining = cl.Sum(c => c.numberRemaining),
                FollowOnCall = ""}).ToList();

            foreach (var item in PinsData)
            {
                var dataforProdName = (from st in Context.ICCProducts where st.ProductCode.ToUpper().Contains(item.ProductCode.ToUpper()) select st).FirstOrDefault();
                item.FollowOnCall = dataforProdName.ProductName;
            }
            
            return Json(new { data = PinsData });
        }

        ////[HttpPost]
        ////[AutoValidateAntiforgeryToken]
        ////public async Task<IActionResult> CreatePin(ICCProductData pin)
        ////{

        ////    Context.ICCProductData.Add(pin);
        ////    await Context.SaveChangesAsync();

        ////    return RedirectToAction(nameof(Pin));
        ////}

        ////[HttpPost]
        ////[AutoValidateAntiforgeryToken]
        ////public async Task<IActionResult> UpdatePin(ICCProductData pin)
        ////{
        ////    Context.ICCProductData.Update(pin);
        ////    await Context.SaveChangesAsync();

        ////    return RedirectToAction(nameof(Pin));
        ////}

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------

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


                if(filePath.Contains("merchants"))
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
                                merchants.Status= "ENABLED";
                                merchants.Balance = 1500;
                                merchants.CreditLimit= 3500;
                                merchants.BalWarning= 300;
                                merchants.Type = "CREDIT";
                                merchants.PinMode = "Online First";
                                merchants.Telephone = words[8];


                                Context.Merchants.Add(merchants);
                            }
                        }

                        await Context.SaveChangesAsync();

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
                    ICCProductStockLevels IccStockLevel = new ICCProductStockLevels();
                    int totalPins = 0;
                    List<ICCProductPins> Allpins = new();

                    //Execute a loop over the rows.
                    foreach (string row in csvData.Split('\n'))
                    {
                        if (!string.IsNullOrEmpty(row))
                        {
                            string[] words = row.Split(',');

                            if (words[0] == "")
                                continue;

                            IccStockLevel.dateCreated = DateTime.Now;

                            IccStockLevel.ProviderCode = words[0];
                            IccStockLevel.ProductCode = words[1];
                            IccStockLevel.FreephoneNumber = words[2];
                            IccStockLevel.GeneralNumber = words[3];
                            IccStockLevel.LondonNumber = words[4];
                            IccStockLevel.HelplineNumber = words[5];
                            IccStockLevel.FollowOnCall = words[5];
                            IccStockLevel.ExpiryPeriod = words[6];                            
                            IccStockLevel.value = words[7];

                            ICCProductPins pintoload = new ICCProductPins();
                            pintoload.Pin = words[8];
                            pintoload.Serial = words[9];
                            totalPins += 1;

                            Allpins.Add(pintoload);

                            pintoload.ICCStk = IccStockLevel;
                            Context.ICCProductPins.Add(pintoload);
                        }
                    }
                    
                    IccStockLevel.numberAdded = totalPins;
                    IccStockLevel.numberRemaining = totalPins;
                    IccStockLevel.Pins = Allpins;                    

                    Context.ICCProductStockLevels.Add(IccStockLevel);

                    await Context.SaveChangesAsync();

                }
                catch (Exception e)
                {
                    var dd = e.StackTrace;
                    var ee = e.InnerException;
                }
            }


            return View();
        }

        public ActionResult PinUpload()
        {
            return View();
        }
    }
}
