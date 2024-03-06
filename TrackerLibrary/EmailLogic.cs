using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using FluentEmail.Smtp;
using FluentEmail.Core;

namespace TrackerLibrary
{
    public static class EmailLogic
    {
        public static void SendEmail(string to, string recipient, string subject, string body)
        {
            MailAddress fromMailAddress = new MailAddress(GlobalConfig.AppKeyLookup("senderEmail"), GlobalConfig.AppKeyLookup("senderDisplayName"));

            SmtpSender client = new SmtpSender(() => new SmtpClient(host: "localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
            });

            Email.DefaultSender = client;

            Email
                .From(fromMailAddress.Address.ToString())
                .To(to)
                .Subject(subject)
                .Body(body)
                .Send();

            //MailMessage mail = new MailMessage();
            //mail.To.Add(to);
            //mail.From = fromMailAddress;
            //mail.Subject = subject;
            //mail.Body = body;
            //mail.IsBodyHtml = true;



            //client.Send(mail);
        }

    }
}
