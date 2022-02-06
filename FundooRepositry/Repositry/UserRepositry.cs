using FundooModels;
using FundooRepositry.Context;
using FundooRepositry.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositry.Repositry
{
    public class UserRepositry:IUserRepositry
    {
        private readonly UserContext context;

        public UserRepositry(UserContext context)
        {
            this.context = context;
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
                    await this.context.SaveChangesAsync();
                    return userLogin;
                }
                return null;
            }
            catch(ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
