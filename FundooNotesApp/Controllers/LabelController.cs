using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using CommonLayer.Models;
using ManagerLayer.Interface;
using ManagerLayer.Service;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelManager labelManager;

        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;

        }


        [HttpPost]
        [Route("CreateNewLabel")]

        public async Task<IActionResult> CreateLabel(CreateLabelModel model)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId").Value);
                var label = await labelManager.CreateLabelAsync(userId, model.LabelName);
                return CreatedAtAction(nameof(CreateLabel), new { userId }, label);
            }
            catch (Exception ex)
            {
                throw ex;


            }




        }







            


    }
}
