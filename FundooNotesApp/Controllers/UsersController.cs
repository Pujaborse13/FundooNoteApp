using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonLayer.Models;
using ManagerLayer.Interface;
using ManagerLayer.Service;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Impl;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserManager userManager;
        private readonly IBus bus;
        private readonly ILogger<UsersController> logger;

        public UsersController(IUserManager userManager, IBus bus, ILogger<UsersController> logger)
        {
            this.userManager = userManager;
            this.bus = bus;
            this.logger = logger;
            
        }


        //httplocal/api/Users/Reg
        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult Register(RegisterModel model)
        {
            var check = userManager.CheckEmail(model.Email);

            if (check)
            {
                return BadRequest(new ResponseModel<UserEntity> { Success = false, Message = "Registration Fails " });
            }

            else {
                var result = userManager.Register(model);
               HttpContext.Session.SetInt32("UserId", result.UserId);     // set Session , logger
              //  HttpContext.Session.GetInt32("UserId");                    // before using any user method


                if (result != null)
                {
                    return Ok(new ResponseModel<UserEntity> { Success = true, Message = "Register Successfully", Data = result });

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
                return Ok(new ResponseModel<string> { Success = true, Message = "Login Successful", Data = user });
            }
            return BadRequest(new ResponseModel<string> { Success = false, Message = "Invalid Email or Password" });
        }




        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string Email)
        {
            try {
                if (userManager.CheckEmail(Email))
                {
                    Send send = new Send();
                    ForgotPasswordModel forgotPasswordModel = userManager.ForgotPassword(Email);
                    send.SendMail(forgotPasswordModel.Email, forgotPasswordModel.Token);

                    Uri uri = new Uri("rabbitmq://localhost/FundooNotesEmailQueue");
                    var endPoint = await bus.GetSendEndpoint(uri);

                    await endPoint.Send(forgotPasswordModel);
                    return Ok(new ResponseModel<string> { Success = true, Message = "Mail send Sucessfully" });
                }
                else
                {

                    return BadRequest(new ResponseModel<string>() { Success = false, Message = "Email not send " });

                }
            }
            catch (Exception ex)
            {
                throw ex;

            }


        }



        [Authorize]
        [HttpPost]
        [Route("ResetPassword")]
        public ActionResult RestPassword(ResetPasswordModel reset)
        {
            try
            {
                string Email = User.FindFirst("EmailID").Value;
                if (userManager.ResetPassword(Email, reset))
                {
                    return Ok(new ResponseModel<string> { Success = true, Message = "Password Changed" });

                }
                else {

                    return BadRequest(new ResponseModel<string> { Success = false, Message = "Password Wrong" });
                }

            }

            catch (Exception ex)
            {
                throw ex;
            }


        }


        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
           
            List<UserEntity> userList = userManager.GetAllUsers();

            if (userList != null)

            {
                return Ok(new { Success = true, Message = "User List Get Successfully", Data = userList });
            }
            else
            {
                return NotFound(new { Success = false, Message = "Users not found" });
            }


        }

        [HttpGet]
        [Route("GetUserByUserId")]
        public IActionResult GetUserByUserId(int userId)
        {
            try
            { 
                UserEntity user = userManager.GetUserByUserId(userId);

                if (user != null)
                {
                    return Ok(new { Success = true, Message = "User Found Successfully", Data = user });
                }
                return BadRequest(new { Success = false, Message = "User not Found" });


            }
            catch (Exception ex)
            {
                logger.LogError(ex.ToString());
                throw ex;

            }

        }


        [HttpGet]
        [Route("GetUserNameStartWithA")]
        public IActionResult GetUserNameStartWithA()
        {
            List<UserEntity> user = userManager.GetUserNameStartWithA();

            if (user != null)
            {
                return Ok(new { Success = true, Message = "Users Found SuccessFully with first letter 'A' ", Data = user });
            }
            else {

                return BadRequest(new { Success = false, Message = "User Not found with First letter A" });
            }
        }


        [HttpGet]
        [Route("TotalUSerCount")]
        public IActionResult TotalUSerCount()
        {
            int userCount = userManager.TotalUserCount();


            if (userCount > 0)
            {
                return Ok(new { Success = true, Message = "get total users count SuccessFully.", Data = userCount });
            }
            else
            {

                return BadRequest(new { Success = false, Message = "Empty list ! No User Found" });
            }
        }



        [HttpGet]
        [Route("GetUsersByASCOrder")]

        public IActionResult GetUsersByASCOrder()
        {

            List<UserEntity> UsersByAsc = userManager.GetUsersByASCOrder();

            if (UsersByAsc != null)
            {
                return Ok(new { Success = true, Message = "User Found Successfully", Data = UsersByAsc });

            }
            else
            {
                return BadRequest(new { Success = false, Message = "User not Found" });

            }

        }




        [HttpGet]
        [Route("GetUsersByDESCOrder")]

        public IActionResult GetUsersByDESCOrder()
        {

            List<UserEntity> UsersByDESC = userManager.GetUsersByDESCOrder();

            if (UsersByDESC != null)
            {
                return Ok(new { Success = true, Message = "User Found Successfully", Data = UsersByDESC });

            }
            else
            {
                return BadRequest(new { Success = false, Message = "User not Found" });

            }

        }





        [HttpGet]
        [Route("GetAverageAgeOfUsers")]
        public IActionResult GetAverageAgeOfUsers()
        {
            double AgeAvgCount = userManager.GetAverageAgeOfUsers();


            if (AgeAvgCount > 0)
            {
                return Ok(new { Success = true, Message = "Average Age Of Users Calculated Successfully", Data = AgeAvgCount });
            }
            else
            {
                return BadRequest(new { Success = false, Message = "Fail to calculate Average Age" });
            }
        }


        [HttpGet]
        [Route("OldestAndYoungestUserAge")]
        public IActionResult OldestAndYoungestUserAge()
        {
            var OldestYoungestUserAge = userManager.OldestAndYoungestUserAge();


            if (OldestYoungestUserAge != null)
            {
                return Ok(new { Success = true, Message = "Oldest and youngest user age retrieved successfully.", Data = OldestYoungestUserAge });
            }
            else {
                return BadRequest(new { Success = false, Message = "Fail to get Oldest and Youngest User Age" });

            }

        }






    }


}


