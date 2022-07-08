using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace PinStoreAPI
{
    public class SendEmail
    {
        public async void SendEmailNow(string subject,string body, string file)
        {
            try
            {
                using (MailMessage mm = new MailMessage("ankit@3rtelecom.co.uk", "ankit@cessoftware.com"))
                {
                    mm.CC.Add(new MailAddress("pritesh@3rtelecom.co.uk"));
                    mm.Subject = subject;
                    mm.Body = body;
                    mm.IsBodyHtml = false;

                    if (file != null)
                    {
                        Attachment data = new Attachment(file, MediaTypeNames.Application.Octet); 
                        mm.Attachments.Add(data);                        
                        await smtpMailerAsync(mm);
                        data.Dispose();
                    }
                    else
                    {
                        await smtpMailerAsync(mm);
                    }
                }
            }
            catch(Exception e)
            {
                Log lg = new Log();
                lg.Logt("------------------------------------------");
                lg.Logt(e.Message + "\n" + e.StackTrace);
               
            }
        }
        
        private async Task smtpMailerAsync(MailMessage mm)
        {
            using (SmtpClient smtp = new SmtpClient())
            {
                //smtp.Host = "mail.3rtelecom.co.uk";                        
                smtp.Host = "192.168.17.6";
                smtp.EnableSsl = false;
                NetworkCredential NetworkCred = new NetworkCredential("ankit@3rtelecom.co.uk", "Jai2014!=");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 25;
                await smtp.SendMailAsync(mm);
            }
        }
    }
}
