using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using TransactionsData.Models;

namespace TransactionsData.Controllers
{
    [Authorize]
    public class SendEmailController : Controller
    {
        public IActionResult Index(EmailModel model)
        {
            using (MailMessage mm = new MailMessage("ankit@3rtelecom.co.uk", "ankit@cessoftware.com"))
            {
                mm.Subject = "TEST Email";
                mm.Body = "Test Email";
                //if (model.Attachment.ContentLength > 0)
                //{
                //    string fileName = Path.GetFileName((string)model.Attachment.FileName);
                //    mm.Attachments.Add(new Attachment(model.Attachment.InputStream, fileName));
                //}
                mm.IsBodyHtml = false;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "mail.3rtelecom.co.uk";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("ankit@3rtelecom.co.uk", "Jai2014!=");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                    ViewBag.Message = "Email sent.";
                }
            }
                return View();
        }
    }
}
