﻿using System;
using System.Threading.Tasks;
using CommonLayer.Models;
using ManagerLayer.Interface;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RepositoryLayer.Entity;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IUserManager userManager;
        private readonly IBus bus;
        
        public UsersController(IUserManager userManager, IBus bus)
        {
            this.userManager = userManager;
            this.bus = bus;
           
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


    }


}
        


