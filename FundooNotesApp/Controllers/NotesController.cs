using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonLayer.Models;
using ManagerLayer.Interface;
using ManagerLayer.Service;
using MassTransit.Initializers.PropertyConverters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Migrations;

namespace FundooNotesApp.Controllers
{

    public class NotesController : ControllerBase
    {
        private readonly INotesManager notesManager;
        private readonly IDistributedCache cache;
        private readonly FundooDBContext context;

        public NotesController(INotesManager notesManager, IDistributedCache cache, FundooDBContext context)
        {
            this.notesManager = notesManager;
            this.cache = cache;
            this.context = context;
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


        [HttpDelete]
        [Route("DeleteNote")]
        public IActionResult DeleteNoteByUserIdAndNoteId(int noteId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId").Value);
                bool isDeleted = notesManager.DeleteNoteByUserIdAndNoteId(userId, noteId);

                if (isDeleted)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Note Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Note Not Found or Deletion Failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPut]
        [Route("UpdateNote")]
        public IActionResult UpdateNote(int noteId, UpdateModel updateModel)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId").Value);

                var updatedNote = notesManager.UpdateNote(userId, noteId, updateModel);

                if (updatedNote != null)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Note updated successfully", Data = updatedNote });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Note not found or user unauthorized" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPut]
        [Route("PinNote")]
        public IActionResult PinNote(int notesId)
        {
            try
            {

                int userId = int.Parse(User.FindFirst("UserId").Value);


                int result = notesManager.PinNote(notesId, userId);

                if (result == 1)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Note pin Successfully" });
                }

                else if (result == 2)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Note unpin Successfully" });
                }

                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Note not found" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        [HttpPut]
        [Route("TrashNote")]
        public IActionResult TrashNote(int notesId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId").Value);
                int result = notesManager.TrashNote(notesId, userId);


                if (result == 1)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Note Trash Successfully" });
                }

                else if (result == 2)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Note remove from  Trash Successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Note not found" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        [HttpPut]
        [Route("ArchiveNote")]
        public IActionResult ArchiveNote(int notesId)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId").Value);
                int result = notesManager.ArchiveNote(notesId, userId);

                if (result == 1)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Note Archive Successfully" });
                }

                else if (result == 2)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Not unarchive Successfully" });
                }

                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Note not found" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPut]
        [Route("AddColour")]
        public IActionResult AddColourInNote(int noteId, string colour)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId").Value);
                bool result = notesManager.AddColourInNote(noteId, colour, userId);

                if (result)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Colour added to Note successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Note not found or unauthorized access" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [HttpPut]
        [Route("AddReminder")]
        public IActionResult AddReminder(int noteId, DateTime reminder)
        {
            try
            {
                int userId = int.Parse(User.FindFirst("UserId").Value);

                bool result = notesManager.AddReminder(noteId, userId, reminder);

                if (result)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Reminder added successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Note not found or unauthorized access" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPut]
        [Route("AddImage")]
        public IActionResult AddImage(int noteId, IFormFile Image)
        {
            try
            {

                int userId = int.Parse(User.FindFirst("UserId").Value);
                bool result = notesManager.AddImage(noteId, userId, Image);
                if (result)
                {
                    return Ok(new ResponseModel<NotesEntity> { Success = true, Message = "Image added successfully" });
                }
                else
                {
                    return BadRequest(new ResponseModel<NotesEntity> { Success = false, Message = "Fail to add Image" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        [HttpGet]
        [Route("GetAllNotesUsingRedisCache")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {

            string cacheKey = "NotesList";
            string SerializedNoteList;
            var NotesList = new List<NotesEntity>();
            byte[] RedisNotesList = await cache.GetAsync(cacheKey);
            if (RedisNotesList != null)
            {
                SerializedNoteList = Encoding.UTF8.GetString(RedisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(SerializedNoteList);
            }
            else
            {

                NotesList = context.Notes.ToList();
                SerializedNoteList = JsonConvert.SerializeObject(NotesList);
                RedisNotesList = Encoding.UTF8.GetBytes(SerializedNoteList);
                var option = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(20))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(5));
                await cache.SetAsync(cacheKey, RedisNotesList, option);
            }
            return Ok(NotesList);


        }

    }
}
