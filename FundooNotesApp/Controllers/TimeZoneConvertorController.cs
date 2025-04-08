using System;
using CommonLayer.Models;
using ManagerLayer.Interface;
using MassTransit.Audit;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeZoneConvertorController : ControllerBase
    {
        private readonly ITimeZoneConvertorManager timeZoneConvertorManager;

        public TimeZoneConvertorController(ITimeZoneConvertorManager timeZoneConvertorManager)
        {
            this.timeZoneConvertorManager = timeZoneConvertorManager;
        }


        [HttpGet]
        [Route("TimeZoneConvertorUsingTimeZoneandDate")]
        //using localSystem TimeZone and Date
        public IActionResult ConvertTimeZoneByDateAndTimeZone(string fromZone, string toZone, DateTime datetime)
        {
            try
            {
                var result = timeZoneConvertorManager.ConvertTimeZone(fromZone, toZone, datetime);

                return Ok(new { ConvertedTime = result });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }



        [HttpGet]
        [Route("ConvertIndiaTimeZoneToAnother")]

        public IActionResult ConvertTimeZone(string AnotherZone)
        {
            try
            { 
                var IndiaZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                var OtherZone = TimeZoneInfo.FindSystemTimeZoneById(AnotherZone);


                var IndiaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, IndiaZone);
                var convertedTime = TimeZoneInfo.ConvertTime(IndiaTime, IndiaZone, OtherZone);

                return Ok(new ResponseModel<DateTime>
                {

                    Success = true,
                    Message = "Time zone converted successfully",
                    Data = convertedTime
                });
            }

            catch (Exception ex)
            {
                throw ex;


            }

        }

    }
}

        

        

