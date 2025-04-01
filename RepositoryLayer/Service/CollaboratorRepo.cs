using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class CollaboratorRepo : ICollaboratorRepo
    {
        private readonly FundooDBContext context;
        private readonly IConfiguration configuration;


        public CollaboratorRepo(FundooDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;


        }

        //Add collaborator to note
        public CollaboratorEntity AddCollaborator(int NoteId, int UserId, string Email)
        {
            var collaborator = context.Notes.FirstOrDefault(n => n.NoteId == NoteId && n.UserId == UserId);


            CollaboratorEntity entity = new CollaboratorEntity();
            entity.NoteId = NoteId;
            entity.UserId = UserId;
            entity.Email = Email;

            context.Collaborator.Add(entity);
            context.SaveChanges();
            return entity;
        }



        //Get All Collaborator
        public List<CollaboratorEntity> GetAllCollaborators(int NoteId, int UserId)
        {
            List<CollaboratorEntity> collaborators = context.Collaborator.ToList();
            return collaborators;
        }

        //Remove from Collaboration
        public bool RemoveCollaborator(int NoteId, int UserId, int CollaboratorId)
        {
            var collaborator = context.Collaborator.FirstOrDefault(c => c.CollaboratorId == CollaboratorId && c.NoteId == NoteId);
            if (collaborator != null)
            {
                context.Collaborator.Remove(collaborator);
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }







    }
    
}
