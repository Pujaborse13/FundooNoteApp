using CommonLayer.Models;
using System;
using ManagerLayer.Interface;
using ManagerLayer.Service;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;
using System.Collections.Generic;

namespace FundooNotesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollaboratorController : Controller
    {
        private readonly ICollaboratorManager collaboratorManager;

        public CollaboratorController(ICollaboratorManager collaboratorManager)
        {
            this.collaboratorManager = collaboratorManager;

        }



        //Add Collaborator to Note
        [HttpPost]
        [Route("AddCollaborator")]
        public IActionResult AddCollaborator(int NoteId, string Email)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);
                var result = collaboratorManager.AddCollaborator(NoteId, UserId, Email);

                if (result != null)
                {
                    return Ok(new ResponseModel<CollaboratorEntity>{Success = true,Message = "Collaborator Added successfully !", Data = result});
                }
                else
                {
                    return BadRequest(new ResponseModel<CollaboratorEntity>{Success = false,Message = "Adding Collaborator Failed !!!!!!",Data = result});
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }



        //To Display all Collaborators
        [HttpGet]
        [Route("GetAllCollaborators")]
        public IActionResult GetAllCollaborators(int NoteId)
        {
            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);
                var result = collaboratorManager.GetAllCollaborators(NoteId, UserId);

                if (result != null)
                {
                    return Ok(new ResponseModel<List<CollaboratorEntity>> {Success = true, Message = "Getting All Collaborator successfully !",Data = result});
                }
                else
                {
                    return BadRequest(new ResponseModel<List<CollaboratorEntity>>{Success = false,Message = " Failed to Get All Collaborator!!!!!!",Data = result});
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }




        //Remove from Collaboration
        [HttpDelete]
        [Route("RemoveCollaborator")]
        public IActionResult RemoveCollaborator(int NoteId, [FromRoute] int CollaboratorId)
        { 
            try
            {
                int UserId = int.Parse(User.FindFirst("UserId").Value);
                var result = collaboratorManager.RemoveCollaborator(NoteId, UserId, CollaboratorId);

                if (result)
                {
                    return Ok(new ResponseModel<bool>{Success = true,Message = "Removed Collaborator successfully !",Data = result});
                }
                else
                {
                    return BadRequest(new ResponseModel<bool>{Success = false,Message = "Failed to Remove userCollaborator !!!!!!",Data = result});
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }









    }



}
