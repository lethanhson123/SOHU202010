﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SOHU.Service.Mail
{
    public class MailService
    {
        public void Send(Mail mail)
        {
            var client = Initialization(mail);
            if (client != null)
            {
                var message = WriteMessage(mail);
                client.Send(message);
            }
        }

        public SmtpClient Initialization(Mail mail)
        {
            if (mail != null)
            {
                SmtpClient client = new SmtpClient()
                {
                    EnableSsl = mail.IsUsingSSL,
                    Host = mail.STMPServer,
                    Port = mail.SMTPPort,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(mail.Username, mail.Password),
                };
                return client;
            }
            return null;
        }

        public MailMessage WriteMessage(Mail mail)
        {
            if (mail != null)
            {
                MailMessage message = new MailMessage()
                {
                    IsBodyHtml = mail.IsMailBodyHtml,
                    Subject = mail.Subject,
                    Body = mail.Content,
                    Priority = MailPriority.High,
                    BodyEncoding = Encoding.GetEncoding("UTF-8"),
                    From = new MailAddress(mail.FromMail, mail.Display),
                };
                message.To.Add(mail.ToMail);
                return message;
            }
            return null;
        }
    }
}
