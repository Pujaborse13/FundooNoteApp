using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using ManagerLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using RepositoryLayer.Service;

namespace ManagerLayer.Service
{
    public class CollaboratorManager : ICollaboratorManager
    {
        private readonly ICollaboratorRepo collaboratorRepo;

        public CollaboratorManager(ICollaboratorRepo collaboratorRepo)
        {
            this.collaboratorRepo = collaboratorRepo;
        }


        //public bool AddCollaborator(int noteId, int userId, string email)
        //{
        //    return collaboratorRepo.AddCollaborator(noteId, userId, email);
        //}

        //Add collaborator to note
        public CollaboratorEntity AddCollaborator(int NoteId, int UserId, string Email)
        {
            return collaboratorRepo.AddCollaborator(NoteId, UserId, Email);
        }


        public List<CollaboratorEntity> GetAllCollaborators(int NoteId, int UserId)
        { 
            return collaboratorRepo.GetAllCollaborators(NoteId, UserId);
        
        }

        //Remove from Collaboration
        public bool RemoveCollaborator(int NoteId, int UserId, int CollaboratorId)
        {
            return collaboratorRepo.RemoveCollaborator(NoteId, UserId, CollaboratorId);
        }




    }
}
