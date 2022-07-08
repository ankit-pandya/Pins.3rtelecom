using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PinStoreAPI.Data;
using System;
using System.IO;
using System.Linq;


namespace PinStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class voucherController : ControllerBase
    {
        public ApplicationDbContext Context { get; }

        public voucherController(ApplicationDbContext context)
        {
            Context = context;
        }

        [HttpGet]
        public string Index()
        {
            return "Connected!";
        }

        private IActionResult View()
        {

            return (IActionResult)GetPin2("");
            // throw new NotImplementedException();
        }


        [HttpPost]
        public PinResponse voucher(PinRequest request)
        {            
            return GetPin(request);
        }


        private PinResponse GetPin2(string request)
        {
            PinResponse p = new PinResponse();
            return p;
        }

        private PinResponse GetPin(PinRequest request)
        {

            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;

            string providerID = request.supplierCode;
            string productID = request.productCode;
            decimal value = request.value;
            decimal merchantBalanceValue = 0;

            var merchant = (from m in Context.Merchants where m.MerchantID == request.mid select m).FirstOrDefault();


            if (merchant != null)
            {
                merchantBalanceValue = merchant.Balance;
                var terminal = (from t in Context.MerchantTerminal where t.MerchantID == request.mid && t.TerminalId == request.tid select t).FirstOrDefault();
                if (terminal == null)
                {
                    return new PinResponse()
                    {
                        code = 21,
                        supplierCode = providerID,
                        productCode = productID,
                        value = value,
                        info = "Invalid Terminal",
                        transactionId = ""
                    };
                }

                if (merchant.Balance <= 0 || merchant.Balance < value / 100)
                {
                    return new PinResponse()
                    {
                        code = 21,
                        supplierCode = providerID,
                        productCode = productID,
                        value = value,
                        info = "Insufficient Credit",
                        transactionId = ""
                    };
                }

            }
            else
            {
                return new PinResponse()
                {

                    code = 21,
                    supplierCode = providerID,
                    productCode = productID,
                    value = value,
                    info = "Invalid Merchant",
                    transactionId = ""

                };
            }

            //var a = (from pins in Context.ICCProductStockLevels where pins.ProviderCode == providerID && pins.ProductCode == productID && pins.value == value select pins).FirstOrDefault();
            var a = (from pins in Context.tblproductdenoms where pins.providerCode == providerID && pins.productCode == productID && pins.denomination == value select pins).FirstOrDefault();
            var c = (from tr in Context.Transactions orderby tr.TransactionID select tr).LastOrDefault();
            //var dd = (from pr in Context.ICCProducts where pr.SupplierID == providerID && pr.ProductCode == productID select pr).FirstOrDefault();
            var d = (from pr in Context.tblproducts where pr.providerCode == providerID && pr.productCode == productID select pr).FirstOrDefault();
            //var ee = (from pr in Context.ICCProviders where pr.SupplierID == providerID select pr).FirstOrDefault();
            var e = (from pr in Context.tblproviders where pr.providerCode == providerID select pr).FirstOrDefault();

            string ProductName = "";
            string ProviderName = "";

            if (e != null)
            {
                ProviderName = e.providerName;
            }

            if (d != null)
            {
                ProductName = d.productName;
            }

            if (merchant != null)
            {
                if (merchant.Status.ToUpper() != "ENABLED")
                {
                    int trIdF = 0;
                    int transIdF = 1;

                    if (c != null && c.TransactionID > 0)
                    {
                        trIdF = c.TransactionID + 1;
                        transIdF = trIdF;
                    }

                    TransactionsModel failedTrans = new TransactionsModel();
                    failedTrans.MerchantID = request.mid;
                    failedTrans.TerminalId = request.tid;
                    failedTrans.ProductCode = request.productCode;
                    failedTrans.ResponseCode = "32";
                    failedTrans.TxnNumber = request.txnNO;
                    failedTrans.Value = request.value;
                    failedTrans.DateandTime = DateTime.Now;
                    failedTrans.TransactionID = transIdF;
                    failedTrans.ProductName = ProductName;
                    failedTrans.Type = "IP";
                    failedTrans.ProviderCode = request.supplierCode;
                    failedTrans.ProviderName = ProviderName;
                    failedTrans.status = "Account Disabled";                    

                    PinResponse response = new PinResponse
                    {
                        code = 32,
                        supplierCode = providerID,
                        productCode = productID,
                        value = value,
                        info = "Account Disabled",
                        transactionId = ""
                    };

                    failedTrans.requestData = request.ToString();
                    failedTrans.responseData = response.ToString();

                    Context.Transactions.Add(failedTrans);
                    Context.SaveChanges();

                    return response;                    
                }
                if (merchant != null)
                {
                    bool lOk = true;
                    PinResponse failedResp = new PinResponse();

                    if (merchant.Balance <= 0 || merchant.Balance < value / 100)
                    {
                        lOk = false;
                        failedResp.code = 32;
                        failedResp.info = "Insufficient Credit";
                        failedResp.supplierCode = providerID;
                        failedResp.productCode = productID;
                    }

                    int day = (int)DateTime.Now.DayOfWeek;
                    int hour = DateTime.Now.Hour;
                    int minute = DateTime.Now.Minute;
                    String currentTime = DateTime.Now.ToString("HH:mm");
                    String openTime = currentTime;
                    String closeTime = currentTime;
                    switch (day)
                    {
                        case 0:
                            currentTime = DateTime.Now.ToString("HH:mm");
                            openTime = merchant.SunOH + ":" + merchant.SunOM;
                            closeTime = merchant.SunCH + ":" + merchant.SunCM;
                            if (DateTime.Parse(currentTime) > DateTime.Parse(closeTime) ||
                                DateTime.Parse(currentTime) < DateTime.Parse(openTime))
                            {
                                lOk = false;
                                failedResp.code = 32;
                                failedResp.info = "Out of Hours";
                                failedResp.supplierCode = providerID;
                                failedResp.productCode = productID;
                                
                            }
                            break;
                        case 1:
                            currentTime = DateTime.Now.ToString("HH:mm");
                            openTime = merchant.MonOH + ":" + merchant.MonOM;
                            closeTime = merchant.MonCH + ":" + merchant.MonCM;
                            if (DateTime.Parse(currentTime) > DateTime.Parse(closeTime) ||
                                DateTime.Parse(currentTime) < DateTime.Parse(openTime))
                            {
                                lOk = false;
                                failedResp.code = 32;
                                failedResp.info = "Out of Hours";
                                failedResp.supplierCode = providerID;
                                failedResp.productCode = productID;
                            }
                            break;
                        case 2:
                            currentTime = DateTime.Now.ToString("HH:mm");
                            openTime = merchant.TueOH + ":" + merchant.TueOM;
                            closeTime = merchant.TueCH + ":" + merchant.TueCM;
                            if (DateTime.Parse(currentTime) > DateTime.Parse(closeTime) ||
                                DateTime.Parse(currentTime) < DateTime.Parse(openTime))
                            {
                                lOk = false;
                                failedResp.code = 32;
                                failedResp.info = "Out of Hours";
                                failedResp.supplierCode = providerID;
                                failedResp.productCode = productID;
                            }
                            break;
                        case 3:
                            currentTime = DateTime.Now.ToString("HH:mm");
                            openTime = merchant.WedOH + ":" + merchant.WedOM;
                            closeTime = merchant.WedCH + ":" + merchant.WedCM;
                            if (DateTime.Parse(currentTime) > DateTime.Parse(closeTime) ||
                                DateTime.Parse(currentTime) < DateTime.Parse(openTime))
                            {
                                lOk = false;
                                failedResp.code = 32;
                                failedResp.info = "Out of Hours";
                                failedResp.supplierCode = providerID;
                                failedResp.productCode = productID;
                            }
                            break;
                        case 4:
                            currentTime = DateTime.Now.ToString("HH:mm");
                            openTime = merchant.ThuOH + ":" + merchant.ThuOM;
                            closeTime = merchant.ThuCH + ":" + merchant.ThuCM;
                            if (DateTime.Parse(currentTime) > DateTime.Parse(closeTime) ||
                                DateTime.Parse(currentTime) < DateTime.Parse(openTime))
                            {
                                lOk = false;
                                failedResp.code = 32;
                                failedResp.info = "Out of Hours";
                                failedResp.supplierCode = providerID;
                                failedResp.productCode = productID;
                            }
                            break;
                        case 5:
                            currentTime = DateTime.Now.ToString("HH:mm");
                            openTime = merchant.FriOH + ":" + merchant.FriOM;
                            closeTime = merchant.FriCH + ":" + merchant.FriCM;

                            if (DateTime.Parse(currentTime) > DateTime.Parse(closeTime) ||
                                DateTime.Parse(currentTime) < DateTime.Parse(openTime))
                            {
                                lOk = false;
                                failedResp.code = 32;
                                failedResp.info = "Out of Hours";
                                failedResp.supplierCode = providerID;
                                failedResp.productCode = productID;
                            }
                            break;
                        case 6:
                            currentTime = DateTime.Now.ToString("HH:mm");
                            openTime = merchant.SatOH + ":" + merchant.SatOM;
                            closeTime = merchant.SatCH + ":" + merchant.SatCM;

                            if (DateTime.Parse(currentTime) > DateTime.Parse(closeTime) ||
                                DateTime.Parse(currentTime) < DateTime.Parse(openTime))
                            {
                                lOk = false;
                                failedResp.code = 32;
                                failedResp.info = "Out of Hours";
                                failedResp.supplierCode = providerID;
                                failedResp.productCode = productID;
                            }
                            break;
                    }

                    if (!lOk)
                    {
                        var cresp = (from tr in Context.Transactions orderby tr.TransactionID select tr).LastOrDefault();

                        int trIdF = 0;
                        int transIdF = 1;

                        if (cresp != null && cresp.TransactionID > 0)
                        {
                            trIdF = cresp.TransactionID + 1;
                            transIdF = trIdF;
                        }

                        TransactionsModel transData = new TransactionsModel();

                        transData.MerchantID = request.mid;
                        transData.TerminalId = request.tid;
                        transData.ProductCode = productID;
                        transData.ResponseCode = failedResp.code.ToString();
                        transData.TxnNumber = request.txnNO;
                        transData.Value = request.value;
                        transData.DateandTime = DateTime.Now;
                        transData.TransactionID = transIdF;
                        transData.ProductName = ProductName;
                        transData.Type = "IP";
                        transData.ProviderCode = providerID;
                        transData.ProviderName = ProviderName;
                        transData.status = failedResp.info;
                        transData.requestData = JsonConvert.SerializeObject(request);
                        transData.responseData = JsonConvert.SerializeObject(failedResp);


                        Context.Transactions.Add(transData);
                        Context.SaveChanges();

                        return failedResp;
                    }
                }
            }

            if (a == null)
            {
                int trIdF = 0;
                int transIdF = 1;

                if (c != null && c.TransactionID > 0)
                {
                    trIdF = c.TransactionID + 1;
                    transIdF = trIdF;
                }

                TransactionsModel failedTrans = new TransactionsModel();
                failedTrans.MerchantID = request.mid;
                failedTrans.TerminalId = request.tid;
                failedTrans.ProductCode = request.productCode;
                failedTrans.ResponseCode = "32";
                failedTrans.TxnNumber = request.txnNO;
                failedTrans.Value = request.value;
                failedTrans.DateandTime = DateTime.Now;
                failedTrans.TransactionID = transIdF;
                failedTrans.ProductName = ProductName;
                failedTrans.Type = "IP";
                failedTrans.ProviderCode = request.supplierCode;
                failedTrans.ProviderName = ProviderName;
                failedTrans.status = "Pin not available";

                PinResponse response = new PinResponse
                {
                    code = 32,
                    supplierCode = providerID,
                    productCode = productID,
                    value = value,
                    info = "Pin not available",
                    transactionId = ""
                };

                failedTrans.requestData = request.ToString();
                failedTrans.responseData = response.ToString();

                Context.Transactions.Add(failedTrans);
                Context.SaveChanges();

                return response;
            }

            var b = (from pins in Context.tblproductpins where pins.productDenominationsid == a.id && pins.transactionId == null select pins).FirstOrDefault();

            if (b == null)
            {
                int trIdF = 0;
                int transIdF = 1;

                if (c != null && c.TransactionID > 0)
                {
                    trIdF = c.TransactionID + 1;
                    transIdF = trIdF;
                }

                TransactionsModel failedTrans = new TransactionsModel();
                failedTrans.MerchantID = request.mid;
                failedTrans.TerminalId = request.tid;
                failedTrans.ProductCode = request.productCode;
                failedTrans.ResponseCode = "32";
                failedTrans.TxnNumber = request.txnNO;
                failedTrans.Value = request.value;
                failedTrans.DateandTime = DateTime.Now;
                failedTrans.TransactionID = transIdF;
                failedTrans.ProductName = ProductName;
                failedTrans.Type = "IP";
                failedTrans.ProviderCode = request.supplierCode;
                failedTrans.ProviderName = ProviderName;
                failedTrans.status = "Pin not available";

                PinResponse response = new PinResponse
                {
                    code = 32,
                    supplierCode = a.providerCode,
                    productCode = a.productCode,
                    value = a.denomination,
                    info = "Pin not available",
                    transactionId = ""
                };

                failedTrans.requestData = request.ToString();
                failedTrans.responseData = response.ToString();

                Context.Transactions.Add(failedTrans);
                Context.SaveChanges();

                return response;
            }

            int trId = 0;
            int transId = 1;

            if (c.TransactionID > 0)
            {
                trId = c.TransactionID + 1;
                transId = trId;
            }

            TransactionsModel trans = new TransactionsModel();
            trans.MerchantID = request.mid;
            trans.TerminalId = request.tid;
            trans.ProductCode = request.productCode;
            trans.ResponseCode = "00";
            trans.status = "Successful";
            trans.TxnNumber = request.txnNO;
            trans.Value = request.value;
            trans.SerialNumber = b.serial;
            trans.DateandTime = DateTime.Now;
            trans.TransactionID = transId;
            trans.ProductName = ProductName;
            trans.Type = "IP";
            trans.ProviderCode = request.supplierCode;
            trans.ProviderName = ProviderName;

            PinResponse resp = new PinResponse
            {
                code = 10,
                supplierCode = a.providerCode,
                productCode = a.productCode,
                value = a.denomination,
                pin = b.pin,
                serial = b.serial,
                helplineNumber = a.helplineNumber,
                info = "",
                transactionId = transId.ToString()
            };
            
            trans.requestData = JsonConvert.SerializeObject(request); ;
            trans.responseData = JsonConvert.SerializeObject(resp); ;

            merchantBalanceValue -= request.value / 100;
            
            merchant.Balance = merchantBalanceValue;
            MerchantBalance mBalance = (from m in Context.tblMerchantBalance where m.MerchantID == request.mid select m).FirstOrDefault();
            mBalance.Balance = merchantBalanceValue;

            Context.tblMerchantBalance.Update(mBalance);
            Context.Merchants.Update(merchant);


            Context.Transactions.Add(trans);

            b.transactionId = trans.TransactionID.ToString();
            b.dateSold = trans.DateandTime;
            a.numberRemaining -= 1;

            Context.Update(a);
            Context.Update(b);
            Context.SaveChanges();

            return resp;
        }
    }
}
