using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using CommonLayer.Models;
using ManagerLayer.Interface;
using ManagerLayer.Service;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;
using System.Reflection.Emit;

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

        [HttpGet]
        [Route("GetLabelByUserId")]
        public async Task<IActionResult>GetLabel()
        {
            try
            {
                int userId = int.Parse(User.FindFirst("userId").Value);
                var result = await labelManager.GetLabelAsync(userId);
                if (result != null)
                {
                    return Ok(new ResponseModel<List<LabelEntity>> { Success = true, Message = "label get successfully !", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<List<LabelEntity>> { Success = false, Message = "fail to get label", Data = result });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPut]
        [Route("AssignLabelByNoteId")]
        public async Task<IActionResult> AssignLabelToNote(int noteId, int labelId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("userId").Value);
                var result = await labelManager.AssignLabelToNoteAsync(noteId, labelId);
                if (result != null)
                {
                    return Ok(new ResponseModel<bool> { Success = true, Message = "label assign successfully !", Data = result });
                }
                else
                {
                    return BadRequest(new ResponseModel<bool> { Success = false, Message = "fail assign label to note", Data = result });
                }
            }
            catch(Exception ex) {
                throw ex;

            }
         }












        
    }
}
