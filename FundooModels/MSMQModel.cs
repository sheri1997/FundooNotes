using System;
using Experimental.System.Messaging;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace FundooModels
{
    public class MSMQModel
    {
        public static void MessageS(string Token)
        {
            MessageQueue messageQueue = new MessageQueue();

            try
            {
                if (!MessageQueue.Exists(@".\Private$\Token")) ;
                {
                    MessageQueue.Create(@".\Private$\Token");
                }
                messageQueue.Formatter = new XmlMessageFormatter(new string[] { "System.String" });
                messageQueue.ReceiveCompleted += recieveMessage;
                messageQueue.Send(Token);
                messageQueue.BeginReceive();
                messageQueue.Close();
            }
            catch (MessageQueueException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private static void recieveMessage(object sender, ReceiveCompletedEventArgs receiveCompletedEventArgs)
        {
            MessageQueue messageQueue = new MessageQueue();
            var msg = messageQueue.EndReceive(receiveCompletedEventArgs.AsyncResult);
            string token = msg.Body.ToString();
            try
            {
                MailMessage mailMessage = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                mailMessage.From = new MailAddress("shvspandey@gmail.com");
                mailMessage.To.Add(new MailAddress("shvspandey@gmail.com"));
                mailMessage.Subject = "Forget Password Link";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = token;
                string jwt = DecodeToken(token);
                smtpClient.Port = 587;
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("shvspandey@gmail.com", "S800.4910.274#");
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                messageQueue.BeginReceive();
            }
        }
        public static string DecodeToken(string Token)
        {
            try
            {
                var stream = Token;
                var handler = new JwtSecurityTokenHandler();
                var decodedToken = handler.ReadJwtToken(stream);
                var result = decodedToken.Claims.FirstOrDefault().Value;
                return result;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    } 
}
