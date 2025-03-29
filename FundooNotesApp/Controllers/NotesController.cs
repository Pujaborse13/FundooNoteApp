using System;
using System.Collections.Generic;
using CommonLayer.Models;
using ManagerLayer.Interface;
using ManagerLayer.Service;
using Microsoft.AspNetCore.Identity;
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
                else { 
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Failed to create note", Data = notesEntity });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
               
        }


        [HttpPost]
        [Route("GetAllNotesById")]
        public IActionResult GetAllNotesById()
        {
               int UserId = int.Parse(User.FindFirst("UserId").Value);
                List<NotesEntity> notesList = notesManager.GetAllNotesByUserId(UserId);

                if (notesList != null)
                {
                    return Ok(new { Success = true, Message = "Notes fetched successfully", Data = notesList });
                }
                else
                {
                    return NotFound(new { Success = false, Message = "No notes found" });
                }
        }



        [HttpGet]
        [Route("FetchNotes")]
        public IActionResult FetchNotes(string title, string description)
        {
            var notes = notesManager.FetchNotes(title, description);

            if (notes != null)
            {
                return Ok(new { Success = true, Message = "Notes Fetched Successfully.", Data = notes });
            }
            else
            { 
                return BadRequest(new { Success = false, Message = "Fail to Fetched Notes " });
            }
        }

        [HttpGet]
        [Route("GetUserNotesCount")]
        public IActionResult GetUserNotesCount()
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);  
                int noteCount = notesManager.GetUserNotesCount(UserId);

                if (noteCount > 0)
                {
                    return Ok(new { Success = true, Message = "User notes count retrieved successfully.", Data = noteCount });
                }
                else
                {
                    return NotFound(new { Success = false, Message = "No notes found for the given user." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "An error occurred.", Error = ex.Message });
            }
        }


    }
}
