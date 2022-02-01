using FundooManager.Interface;
using FundooModels;
using FundooRepositry.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Manager
{
    public class UserManager: IUserManager
    {
        private readonly IUserRepositry repositry;
        public UserManager(IUserRepositry repositry)
        {
            this.repositry = repositry;
        }
        public async Task<UserModel> Register(UserModel userData)
        {
            try
            {
                return await this.repositry.Register(userData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<UserLogin> Login(UserLogin userLogin)
        {
            try
            {
                return await this.repositry.Login(userLogin);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
