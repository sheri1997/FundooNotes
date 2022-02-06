using FundooManager.Interface;
using FundooRepositry.Repositry;
using FundooModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

        [HttpPost]
        [Route("api/login")]
        public async Task<IActionResult> Login([FromBody] UserLogin userLogin)
        {
            //var handler = new JwtSecurityTokenHandler();
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
        [HttpPost]
        [Route("api/forgetpassword")]
        public IActionResult ForgetPassword(string Email)
        {
            //var handler = new JwtSecurityTokenHandler();
            try
            {
                var result = this.manager.forgetPassword(Email);
                if (result != null)
                {
                    return this.Ok(new { Status = true, Message = "Email Send Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Password Not Changed Successfully", Data = result });
                }
            }
            catch (Exception ex)
            {
                return this.NotFound(new { Status = false, ex.Message });
            }
        }
        [HttpPost]
        [Route("api/resetpassword")]
        public IActionResult ResetPassword(string email, string password)
        {
            try
            {
                var checkuser = this.User.Claims.First(p => p.Type == email).Value;
                UserRepositry userRepositry = null;
                userRepositry.ResetPassword(checkuser, password);
                return Ok(new { Status = true, Message = "Password Reset Successfully" });
            }
            catch(Exception)
            {
                return BadRequest(new { Status = false, Message = "Password Cannot Be Changed" });
            }
        }

    }
}
