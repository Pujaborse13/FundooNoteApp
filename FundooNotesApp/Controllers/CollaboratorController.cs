using CommonLayer.Models;
using System;
using ManagerLayer.Interface;
using ManagerLayer.Service;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entity;

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
                    return BadRequest(new ResponseModel<CollaboratorEntity>{Success = true,Message = "Adding Collaborator Failed !!!!!!",Data = result});
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }








    }



}
