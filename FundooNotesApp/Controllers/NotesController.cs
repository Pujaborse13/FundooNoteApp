using System;
using CommonLayer.Models;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;

namespace FundooNotesApp.Controllers
{

    public class NotesController : ControllerBase
    {
        private readonly INotesManager notesManager;
        public NotesController(INotesManager notesManager)
        { 
            this.notesManager = notesManager;
        }





        [HttpPost]
        [Route("CreateNote")]
        public IActionResult CreateNote(NotesModel notesModel)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);
                NotesEntity notesEntity = notesManager.CreateNote(UserId, notesModel);


                if (notesEntity != null)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Note Created Successfully", Data = notesEntity });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Failed to create note", Data = notesEntity });

                }



            }
            catch (Exception ex)
            {
                throw ex;            
            }



               
        }


    }
}
