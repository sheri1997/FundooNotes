using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositry.Interface
{
    public interface IUserRepositry
    {
        Task<UserModel> Register(UserModel userData);
        Task<UserLogin> Login(UserLogin userLogin);
        //Task<MSMQModel> forgetPassword(MSMQModel mSMQModel);
        public string forgetPassword(string email);
        public bool ResetPassword(string email, string Password);

    }
}
