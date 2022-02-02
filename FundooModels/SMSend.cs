using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace FundooModels
{
    public class SMSend
    {
        MessageQueue MSMQ = new MessageQueue();
        public void Sender(string token)
        {

            MSMQ.Path = @".\private$\Tokens";

            try
            {
                if (!MessageQueue.Exists(MSMQ.Path))
                {
                    MessageQueue.Create(MSMQ.Path);
                }

                MSMQ.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });

                MSMQ.ReceiveCompleted += Msmq_ReceiveCompleted;
                MSMQ.Send(token);
                MSMQ.BeginReceive();
                MSMQ.Close();
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public static string GetEmailFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decoded = handler.ReadJwtToken((token));
            var result = decoded.Claims.FirstOrDefault().Value;
            return result;
        }
        private void Msmq_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = MSMQ.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();


            // mail sending code smtp
            string mailReceiver = GetEmailFromToken(token).ToString();
            MailMessage message = new MailMessage("shreeshbri@gmail.com", mailReceiver);
            string bodymessage = $"<a href=https://localhost:44349/swagger/index.html click me</a>" +
            "Token-> : " + token;



            message.Subject = "Message";
            message.Body = bodymessage;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            System.Net.NetworkCredential basicCredential1 = new
            System.Net.NetworkCredential("shreeshbri@gmail.com", "S800.4910.274@");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(message);
            }

            catch (Exception ex)
            {
                throw;
            }

            MSMQ.BeginReceive();
        }
     }
}
