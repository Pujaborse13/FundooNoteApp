using CommonLayer.Models;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserManager userManager;

        public UsersController(IUserManager userManager)
        {
            this.userManager = userManager;
        }


        //httplocal/api/Users/Reg
        [HttpPost]
        [Route("Reg")]
        public IActionResult Register(RegisterModel model)
        {
            var check = userManager.CheckEmail(model.Email);
            if (check)
            {
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Registration Fails " });
            }

            else { 
                var result = userManager.Register(model);
                if (result != null)
                {
                     return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Register Sucsessfully", Data = result });
    
                }
                 return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Register Fail", Data = result });


            }


        }


        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel model)
        {
            var user = userManager.Login(model);
            if (user != null)
            {
                return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Login Successful", Data = user });
            }
            return Unauthorized(new ResponseModel<UserEntity> { Success = false, Message = "Invalid Email or Password" });
        }







    }
}