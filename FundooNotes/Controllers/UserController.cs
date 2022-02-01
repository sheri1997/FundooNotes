using FundooManager.Interface;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace FundooNotes.Controllers
{
    public class UserController : ControllerBase
    {
        private readonly IUserManager manager;
        public UserController(IUserManager manager)
        {
            this.manager = manager;
        }
        [HttpPost]
        [Route("api/register")]
        public async Task<IActionResult> Register([FromBody] UserModel userData)
        {
            try
            {
                var result = await this.manager.Register(userData);
                if(result!= null)
                {
                    return this.Ok(new { Status = true, Message = "Register Successful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Register Unsuccessful" });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new {Status = false, ex.Message});
            }
        }

        [HttpGet]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var result = await this.manager.Login(userLogin);
                if(result!=null)
                {
                    return this.Ok(new { Status = true, Message = "Login Successful", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Login UnSuccessful", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
    }
}
