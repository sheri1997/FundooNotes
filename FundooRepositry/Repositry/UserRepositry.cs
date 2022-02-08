using FundooModels;
using FundooRepositry.Context;
using FundooRepositry.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Experimental.System.Messaging;
using System.Net.Mail;
using System.Net;

namespace FundooRepositry.Repositry
{
    public class UserRepositry : IUserRepositry
    {
        private readonly UserContext context;
        private readonly IConfiguration configuration;

        public UserRepositry(UserContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }
        private string JSONWebToken(string Email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Email", Email) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<UserModel> Register(UserModel userData)
        {
            try
            {
                var checkUser = this.context.User.Where(x => x.Email == userData.Email).FirstOrDefault();
                if (checkUser == null)
                {
                    userData.Password = EncodePasswordToBase64(userData.Password);
                    this.context.User.Add(userData);
                    await this.context.SaveChangesAsync();
                    return userData;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserLogin> Login(UserLogin userLogin)
        {
            try
            {
                var ispassword = userLogin.Password = EncodePasswordToBase64(userLogin.Password);
                var checkUser = this.context.User.Where(x => x.Email == userLogin.Email && x.Password == ispassword).FirstOrDefault();
                if (checkUser != null)
                {
                    userLogin.Password = EncodePasswordToBase64(userLogin.Password);
                    userLogin.Email = JSONWebToken(userLogin.Email);
                    await this.context.SaveChangesAsync();
                    return userLogin;
                }
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public string forgetPassword(string email)
        {
            var url = "Click on following link to reset the password for FundooNotes App:";
            MessageQueue msmqQueue = new MessageQueue();
            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                msmqQueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                msmqQueue = MessageQueue.Create(@".\Private$\MyQueue");

            }
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            message.Body = url;
            msmqQueue.Label = "url link";
            msmqQueue.Send(message);
            var reciever = new MessageQueue(@".\Private$\MyQueue");
            var recieving = reciever.Receive();
            recieving.Formatter = new BinaryMessageFormatter();
            string linkToBeSend = recieving.Body.ToString();

            string user;
            string mailSubject = "Link to reset your FundooNotes App Credentials";
            var userCheck = this.context.User
                            .SingleOrDefault(x => x.Email == email);
            if (userCheck != null)
            {
                string Token = JSONWebToken(userCheck.Email);
                user = linkToBeSend;
                using (MailMessage mailMessage = new MailMessage("shreeshbri@gmail.com", email))
                {
                    mailMessage.Subject = mailSubject;
                    mailMessage.Body = Token;
                    mailMessage.IsBodyHtml = true;
                    SmtpClient Smtp = new SmtpClient();
                    Smtp.Host = "smtp.gmail.com";
                    Smtp.EnableSsl = true;
                    Smtp.UseDefaultCredentials = false;
                    Smtp.Credentials = new NetworkCredential("shreeshbri@gmail.com", "S800.4910.274@");
                    Smtp.Port = 587;
                    Smtp.Send(mailMessage);
                }
                return "Mail Sent Successfully !";
            }
            else
            {
                return "Error while sending mail !";
            }
        }
        public bool ResetPassword(string email, string Password)
        {
            try
            {
                var checkUser = this.context.User.Where(x => x.Email == email).FirstOrDefault();
                if(checkUser != null)
                {
                    checkUser.Password = Password;
                    checkUser.Password = EncodePasswordToBase64(checkUser.Password);
                    this.context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception)
            {
                return false;
            }
        }

    }
}
