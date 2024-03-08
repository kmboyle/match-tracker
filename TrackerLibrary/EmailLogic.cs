using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using FluentEmail.Smtp;
using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace TrackerLibrary
{
    public static class EmailLogic
    {

        public static void SendEmail(string to, string subject, string body)
        {
            SendEmail(new List<string> { to }, new List<string>(), subject, body);
        }
        public static void SendEmail(List<string> to, List<string> bcc, string subject, string body)
        {
            MailAddress fromMailAddress = new MailAddress(GlobalConfig.AppKeyLookup("senderEmail"), GlobalConfig.AppKeyLookup("senderDisplayName"));

            SmtpSender client = new SmtpSender(() => new SmtpClient(host: "localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Port = 25
            });

            Email.DefaultSender = client;

            string toList = "";
            foreach (string email in to)
            {
                toList += $"{email};";
            }
            if (toList.Length > 0)
            {
                toList = toList.Remove(toList.Length - 1, 1);
            }

            List<Address> bccList = new List<Address>();
            foreach (string email in bcc)
            {
                Address address = new Address();
                address.EmailAddress = email;
                bccList.Add(address);
            }

            if (bccList.Count > 0 && toList.Length > 0)
            {
                Email
                .From(fromMailAddress.Address.ToString())
                .To(toList)
                .BCC(bccList)
                .Subject(subject)
                .Body(body)
                .Send();
            }
            else if (bccList.Count > 0)
            {
                Email
                .From(fromMailAddress.Address.ToString())
                .BCC(bccList)
                .Subject(subject)
                .Body(body)
                .Send();

            } else if (toList.Length > 0)
            {
                Email
                .From(fromMailAddress.Address.ToString())
                .To(toList)
                .Subject(subject)
                .Body(body)
                .Send();

            }


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
