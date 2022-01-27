using FundooModels;
using FundooRepositry.Context;
using FundooRepositry.Interface;
using FundooManager.Interface;
using System;
using System.Collections.Generic;
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
        public async Task<UserModel> Register(UserModel userData)
        {
            try
            {
                var checkUser = this.context.User.Where(x => x.Email == userData.Email).FirstOrDefault();
                if (checkUser == null)
                {
                    this.context.User.Add(userData);
                    await this.context.SaveChangesAsync();
                    return userData;
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<UserModel> Register(UserModel userData)
        {
            throw new NotImplementedException();
        }
    }
}
