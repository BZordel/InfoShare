using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using Tingle_WebForms.Models;
using Tingle_WebForms.Logic;
using System.Data.Entity;

namespace Tingle_WebForms.Logic
{
    public class SendEmail
    {
        public Boolean SendMail(string from, string to, string subject, string bodyHtml, TForm newForm, bool sendToCC)
        {
            try
            {
                MailMessage completeMessage = new MailMessage(from, to, subject, bodyHtml);
                completeMessage.IsBodyHtml = true;

                FormContext ctx = new FormContext();

                ICollection<EmailAddress> emailAddresses = ctx.EmailAddresses.Where(ea => ea.Status == 1 && ea.TForm.FormID == newForm.FormID).ToList();

                if (emailAddresses.Count() > 0 && sendToCC == true)
                {
                    foreach (EmailAddress email in emailAddresses)
                    {
                        completeMessage.CC.Add(email.Address);
                    }
                }

                //SmtpClient client = new SmtpClient("TingleNT30.wctingle.com");
                //client.UseDefaultCredentials = true;
                //client.EnableSsl = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new NetworkCredential("rzordel@gmail.com", "ZXCasdQWE123!");
                client.EnableSsl = true;

                client.Send(completeMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
    }
}