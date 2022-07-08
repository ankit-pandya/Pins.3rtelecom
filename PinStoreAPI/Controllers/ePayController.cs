using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PinStoreAPI.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace PinStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ePayController : ControllerBase
    {
        public ApplicationDbContext Context { get; }
        public IConfiguration Configuration { get; }

        public ePayController(ApplicationDbContext context, IConfiguration configuration)
        {
            Context = context;
            Configuration = configuration;
        }

        [HttpGet]
        public string Index()
        {
            return "Connected!";
        }



        [HttpPost]
        public async Task<ePayResponse> ePayvoucherAsync(Request request)
        {
            string MerchantName = "";
            bool lStockFirst = false;

            var merchantDetails = Context.Merchants.Where(m => m.MerchantID == request.mid).FirstOrDefault();
            
            if(merchantDetails != null)
            {
                MerchantName = merchantDetails.Name;
            }

            if (MerchantName != "" && merchantDetails.emailbulk)
            {
                var tData = await Context.Transactions.Where(T => T.MerchantID == request.mid && T.TerminalId == request.tid  && T.DateandTime >= DateTime.Now.AddMinutes(-5) && T.DateandTime <= DateTime.Now).ToListAsync();

                if (tData.Count > 10)
                {
                    SendEmail se = new SendEmail();
                    
                    string body = $"Merchant {request.mid.ToString()} ({MerchantName}) have completed {tData.Count.ToString()} transactions in the last 5 minutes! " +
                                  $"\n\n\n Thank you, \n 3R Pinstore";

                    se.SendEmailNow("WARNING! Excess transactions detected", body, null);
                }
            }

            var tidTXN = (from tr in Context.Transactions where tr.MerchantID == request.mid && tr.TerminalId == request.tid orderby tr.TransactionID select tr).LastOrDefault();
            string txnforTID = "0001";

            if (request.txnNO == "0000")
            {
                if (tidTXN != null)
                {
                    var txnValue = int.Parse(tidTXN.TxnNumber) + 1;
                    txnforTID = txnValue.ToString().PadLeft(4, '0');
                }
            }
            else
            {
                txnforTID = request.txnNO;
            }
            
            request.txnNO = txnforTID;

            var remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;

            string merchantID = request.mid.ToString();
            string terminalID = request.tid;
            int value = request.epayrequest.amount;
            decimal merchantBalanceValue = 0;
            bool isTopup = false;
            string TopupCardNumber = "";

            var merchant = (from m in Context.Merchants where m.MerchantID == request.mid select m).FirstOrDefault();
            if (merchant != null)
            {
                if (merchant.PinMode.ToUpper() == "STOCK FIRST")
                    lStockFirst = true;

                merchantBalanceValue = merchant.Balance;
                var terminal = (from t in Context.MerchantTerminal where t.MerchantID == request.mid && t.TerminalId == request.tid select t).FirstOrDefault();

                if (terminal == null)
                {
                    ePayResponse failedResp = new ePayResponse();
                    failedResp.result = 10;
                    failedResp.resulttext = "Invalid Terminal";

                    return failedResp;
                }
            }
            else
            {
                ePayResponse failedResp = new ePayResponse();
                failedResp.result = 10;
                failedResp.resulttext = "Invalid Merchant";

                return failedResp;
            }

            var MTProduct = (from mt in Context.tblMTProducts where mt.productCode3r == request.epayrequest.productId && mt.value == request.epayrequest.amount select mt).FirstOrDefault();
            
            if (MTProduct == null)
            {                
                MTProduct = (from mt in Context.tblMTProducts where mt.productCode3r == request.epayrequest.productId.Substring(0, 7) && mt.value == request.epayrequest.amount select mt).FirstOrDefault();
                if (MTProduct != null)
                {
                    isTopup = true;
                    TopupCardNumber = request.epayrequest.productId;
                }
            }
            
            if (merchant != null && MTProduct != null)
            {
                bool lOk = true;
                ePayResponse failedResp = new ePayResponse();

                if (merchant.allowEE == false)
                {
                    if(MTProduct.productName.Contains("EE"))
                    {
                        lOk = false;
                        failedResp.result = 10;
                        failedResp.resulttext = "Product Not Allowed";
                    }
                }
                
                

                if (merchant.Balance <= 0 || merchant.Balance < value / 100)
                {
                    lOk = false;
                    failedResp.result = 10;
                    failedResp.resulttext = "Insufficient Credit";
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
                            failedResp.result = 10;
                            failedResp.resulttext = "Out of Hours";
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
                            failedResp.result = 10;
                            failedResp.resulttext = "Out of Hours";
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
                            failedResp.result = 10;
                            failedResp.resulttext = "Out of Hours";
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
                            failedResp.result = 10;
                            failedResp.resulttext = "Out of Hours";
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
                            failedResp.result = 10;
                            failedResp.resulttext = "Out of Hours";
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
                            failedResp.result = 10;
                            failedResp.resulttext = "Out of Hours";
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
                            failedResp.result = 10;
                            failedResp.resulttext = "Out of Hours";
                        }                        
                        break;
                }

                if (!lOk)
                {
                    var c = (from tr in Context.Transactions orderby tr.TransactionID select tr).LastOrDefault();

                    int trIdF = 0;
                    int transIdF = 1;

                    if (c != null && c.TransactionID > 0)
                    {
                        trIdF = c.TransactionID + 1;
                        transIdF = trIdF;
                    }

                    TransactionsModel transData = new TransactionsModel();

                    transData.MerchantID = request.mid;
                    transData.TerminalId = request.tid;
                    transData.ProductCode = request.epayrequest.productId;
                    transData.ResponseCode = failedResp.result.ToString();
                    transData.TxnNumber = request.txnNO;
                    transData.Value = request.epayrequest.amount;
                    transData.DateandTime = DateTime.Now;
                    transData.TransactionID = transIdF;
                    transData.ProductName = MTProduct.productName;
                    transData.Type = "IP";
                    transData.ProviderCode = request.epayrequest.productId;
                    transData.ProviderName = MTProduct.productName;
                    transData.status = failedResp.resulttext;
                    transData.requestData = JsonConvert.SerializeObject(request);
                    transData.responseData = JsonConvert.SerializeObject(failedResp);


                    Context.Transactions.Add(transData);
                    Context.SaveChanges();

                    return failedResp;
                }
            }

            if (MTProduct != null)
            {
                var _url = Configuration["ePAY:url"];
                var _username = Configuration["ePAY:username"];
                var _password = Configuration["ePAY:password"];
                var _retailerid = Configuration["ePAY:retailerid"];
                var _terminalid = Configuration["ePAY:terminalid"];

                request.epayrequest.retailerId = _retailerid;
                request.epayrequest.terminalId = _terminalid;
                request.epayrequest.authorization.userName = _username;
                request.epayrequest.authorization.password = _password;
                
                request.epayrequest.productId = MTProduct.productCode;

                if(isTopup)
                {
                    lStockFirst = false;
                    Card cardPan = new Card { PAN = TopupCardNumber/*"8944111111111111094"*/ };                    
                    request.epayrequest.card = cardPan;
                }
                                
                request.epayrequest.txid = request.txnNO;
                ePayResponse resp = new ePayResponse();

                if (lStockFirst)
                {
                    var a = (from pins in Context.tblproductdenoms where pins.providerCode == MTProduct.provider3r && pins.productCode == MTProduct.product3r && pins.denomination == decimal.Parse(MTProduct.value.ToString()) select pins).FirstOrDefault();
                    
                    if (a != null)
                    {
                        var b = (from pins in Context.tblproductpins where pins.productDenominationsid == a.id && pins.transactionId == null select pins).FirstOrDefault();
                        if (b != null)
                        {
                            var cTransaction = (from tr in Context.Transactions orderby tr.TransactionID select tr).LastOrDefault();

                            int trIdFstock = 0;
                            int transIdFstock = 1;

                            if (cTransaction != null && cTransaction.TransactionID > 0)
                            {
                                trIdFstock = cTransaction.TransactionID + 1;
                                transIdFstock = trIdFstock;
                            }

                            TransactionsModel transDataStock = new TransactionsModel();

                            transDataStock.MerchantID = request.mid;
                            transDataStock.TerminalId = request.tid;
                            transDataStock.ProductCode = a.productCode;
                            transDataStock.ResponseCode = "00";
                            transDataStock.TxnNumber = request.txnNO;
                            transDataStock.Value = request.epayrequest.amount;
                            transDataStock.DateandTime = DateTime.Now;
                            transDataStock.TransactionID = transIdFstock;
                            transDataStock.ProductName = MTProduct.productName;
                            transDataStock.Type = "IP";
                            transDataStock.ProviderCode = a.providerCode;
                            transDataStock.ProviderName = MTProduct.productName;
                            transDataStock.status = "Successful";
                            transDataStock.requestData = JsonConvert.SerializeObject(request);

                            ePayPINCREDENTIALS PinData = new ePayPINCREDENTIALS();
                            PinData.pin = b.pin;
                            PinData.serial = b.serial;

                            resp.PINCREDENTIALS = PinData;
                            resp.amount = request.epayrequest.amount;
                            resp.resulttext = "OK";

                            transDataStock.responseData = JsonConvert.SerializeObject(resp);

                            merchantBalanceValue -= value / 100;
                            merchant.Balance = merchantBalanceValue;

                            MerchantBalance mBalance = (from m in Context.tblMerchantBalance where m.MerchantID == request.mid select m).FirstOrDefault();
                            mBalance.Balance = merchantBalanceValue;

                            Context.tblMerchantBalance.Update(mBalance);
                            Context.Merchants.Update(merchant);

                            transDataStock.requestData = JsonConvert.SerializeObject(request);
                            transDataStock.responseData = JsonConvert.SerializeObject(resp);

                            Context.Transactions.Add(transDataStock);

                            Context.SaveChanges();

                            return resp;
                        }
                        else
                        {
                            var cTransaction = (from tr in Context.Transactions orderby tr.TransactionID select tr).LastOrDefault();

                            int trIdFstock = 0;
                            int transIdFstock = 1;

                            if (cTransaction != null && cTransaction.TransactionID > 0)
                            {
                                trIdFstock = cTransaction.TransactionID + 1;
                                transIdFstock = trIdFstock;
                            }

                            resp.result = 10;
                            resp.resulttext = "Pin not Available";

                            TransactionsModel transDataStock = new TransactionsModel();

                            transDataStock.MerchantID = request.mid;
                            transDataStock.TerminalId = request.tid;
                            transDataStock.ProductCode = request.epayrequest.productId;
                            transDataStock.ResponseCode = resp.result.ToString();
                            transDataStock.TxnNumber = request.txnNO;
                            transDataStock.Value = request.epayrequest.amount;
                            transDataStock.DateandTime = DateTime.Now;
                            transDataStock.TransactionID = transIdFstock;
                            transDataStock.ProductName = "";
                            transDataStock.Type = "IP";
                            transDataStock.ProviderCode = request.epayrequest.productId;
                            transDataStock.ProviderName = "";
                            transDataStock.status = resp.resulttext;
                            transDataStock.requestData = JsonConvert.SerializeObject(request);
                            transDataStock.responseData = JsonConvert.SerializeObject(resp);


                            Context.Transactions.Add(transDataStock);
                            Context.SaveChanges();

                            return resp;
                        }
                    }
                    else
                    {
                        var cTransaction = (from tr in Context.Transactions orderby tr.TransactionID select tr).LastOrDefault();

                        int trIdFstock = 0;
                        int transIdFstock = 1;

                        if (cTransaction != null && cTransaction.TransactionID > 0)
                        {
                            trIdFstock = cTransaction.TransactionID + 1;
                            transIdFstock = trIdFstock;
                        }
                        
                        resp.result = 10;
                        resp.resulttext = "Invalid Product Code";

                        TransactionsModel transDataStock = new TransactionsModel();

                        transDataStock.MerchantID = request.mid;
                        transDataStock.TerminalId = request.tid;
                        transDataStock.ProductCode = request.epayrequest.productId;
                        transDataStock.ResponseCode = resp.result.ToString();
                        transDataStock.TxnNumber = request.txnNO;
                        transDataStock.Value = request.epayrequest.amount;
                        transDataStock.DateandTime = DateTime.Now;
                        transDataStock.TransactionID = transIdFstock;
                        transDataStock.ProductName = "";
                        transDataStock.Type = "IP";
                        transDataStock.ProviderCode = request.epayrequest.productId;
                        transDataStock.ProviderName = "";
                        transDataStock.status = resp.resulttext;
                        transDataStock.requestData = JsonConvert.SerializeObject(request);
                        transDataStock.responseData = JsonConvert.SerializeObject(resp);


                        Context.Transactions.Add(transDataStock);
                        Context.SaveChanges();

                        return resp;

                    }                    
                }
                else
                {
                    HttpClient client = new HttpClient();
                    client.Timeout = TimeSpan.FromSeconds(5);

                    string json = JsonConvert.SerializeObject(request.epayrequest);
                    StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(_url, httpContent);
                    var a = response.StatusCode;
                    var aa = response.RequestMessage.Content.ReadAsStringAsync();
                    var contents = response.Content.ReadAsStringAsync().Result;

                    resp = JsonConvert.DeserializeObject<ePayResponse>(contents);
                }
                
                var c = (from tr in Context.Transactions orderby tr.TransactionID select tr).LastOrDefault();
                

                int trIdF = 0;
                int transIdF = 1;
                

                if (c != null && c.TransactionID > 0)
                {
                    trIdF = c.TransactionID + 1;
                    transIdF = trIdF;
                }

                

                TransactionsModel transData = new TransactionsModel();

                if (resp.resulttext == "OK")
                {
                    transData.MerchantID = request.mid;
                    transData.TerminalId = request.tid;
                    transData.ProductCode = request.epayrequest.productId;
                    transData.ResponseCode = "00";
                    transData.TxnNumber = request.txnNO;
                    transData.Value = request.epayrequest.amount;
                    transData.DateandTime = DateTime.Now;
                    transData.TransactionID = transIdF;
                    transData.ProductName = MTProduct.productName;
                    transData.Type = "IP";
                    transData.ProviderCode = request.epayrequest.productId;
                    transData.ProviderName = MTProduct.productName;
                    transData.status = "Successful";
                    transData.requestData = JsonConvert.SerializeObject(request);
                    transData.responseData = JsonConvert.SerializeObject(resp);

                    merchantBalanceValue -= value / 100;
                    merchant.Balance = merchantBalanceValue;

                    MerchantBalance mBalance = (from m in Context.tblMerchantBalance where m.MerchantID == request.mid select m).FirstOrDefault();
                    mBalance.Balance = merchantBalanceValue;

                    Context.tblMerchantBalance.Update(mBalance);
                    Context.Merchants.Update(merchant);
                }
                else
                {
                    transData.MerchantID = request.mid;
                    transData.TerminalId = request.tid;
                    transData.ProductCode = request.epayrequest.productId;
                    transData.ResponseCode = resp.result.ToString();
                    transData.TxnNumber = request.txnNO;
                    transData.Value = request.epayrequest.amount;
                    transData.DateandTime = DateTime.Now;
                    transData.TransactionID = transIdF;
                    transData.ProductName = MTProduct.productName;
                    transData.Type = "IP";
                    transData.ProviderCode = request.epayrequest.productId;
                    transData.ProviderName = MTProduct.productName;
                    transData.status = resp.resulttext;
                    transData.requestData = JsonConvert.SerializeObject(request);
                    transData.responseData = JsonConvert.SerializeObject(resp);
                }

                Context.Transactions.Add(transData);
                Context.SaveChanges();

                return resp;
            }
            else
            {
                var c = (from tr in Context.Transactions orderby tr.TransactionID select tr).LastOrDefault();

                int trIdF = 0;
                int transIdF = 1;

                if (c != null && c.TransactionID > 0)
                {
                    trIdF = c.TransactionID + 1;
                    transIdF = trIdF;
                }
                ePayResponse resp = new ePayResponse();
                resp.result = 10;
                resp.resulttext = "Invalid Product Code";

                TransactionsModel transData = new TransactionsModel();

                transData.MerchantID = request.mid;
                transData.TerminalId = request.tid;
                transData.ProductCode = request.epayrequest.productId;
                transData.ResponseCode = resp.result.ToString();
                transData.TxnNumber = request.txnNO;
                transData.Value = request.epayrequest.amount;
                transData.DateandTime = DateTime.Now;
                transData.TransactionID = transIdF;
                transData.ProductName = "";
                transData.Type = "IP";
                transData.ProviderCode = request.epayrequest.productId;
                transData.ProviderName = "";
                transData.status = resp.resulttext;
                transData.requestData = JsonConvert.SerializeObject(request);
                transData.responseData = JsonConvert.SerializeObject(resp);


                Context.Transactions.Add(transData);
                Context.SaveChanges();

                return resp;
            }
        }
    }
}
