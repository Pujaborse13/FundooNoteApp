using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface ICollaboratorRepo
    {

        //Add collaborator to note
        public CollaboratorEntity AddCollaborator(int NoteId, int UserId, string Email);

        //Get All Collaborator
        public List<CollaboratorEntity> GetAllCollaborators(int NoteId, int UserId);

        //Remove from Collaboration
        public bool RemoveCollaborator(int NoteId, int UserId, int CollaboratorId);







    }
}
