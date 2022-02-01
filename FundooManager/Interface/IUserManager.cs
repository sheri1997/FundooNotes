using FundooModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooManager.Interface
{
    public interface IUserManager
    {
        Task<UserModel> Register(UserModel userData);
        Task<UserLogin> Login(UserLogin userLogin);
        Task<MSMQModel> forgetPassword(MSMQModel mSMQModel);
        
    }
}
