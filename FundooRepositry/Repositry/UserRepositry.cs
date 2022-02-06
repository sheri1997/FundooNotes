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
        public static dynamic DecodePasswordFromBase64(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var decodedValue = handler.ReadJwtToken(token);
                return decodedValue;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        private string JSONWebToken(string Email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(this.configuration["Jwt:Issuer"], null, expires: DateTime.Now.AddMinutes(120), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
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
        public  string forgetPassword(string email)
        {
            try
            {
                var checkUser = context.User.FirstOrDefault(e => e.Email == email);
                if (checkUser != null)
                {
                    //MSMQModel mSMQModel = new MSMQModel();
                    var token = JSONWebToken(email);
                    //await this.context.SaveChangesAsync();
                    SMSend sMSend = new SMSend();
                    sMSend.Sender(token);
                    return token;
                }
                else
                {
                    return "User Not Found";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
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
