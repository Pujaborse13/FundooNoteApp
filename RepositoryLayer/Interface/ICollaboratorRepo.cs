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
        public List<CollaboratorEntity> GetAllCollaborators(int NoteId);

        //Remove from Collaboration By CollaboratorId
        public bool RemoveCollaborator(int CollaboratorId);



        //Remove from Collaboration By NoteID and Collaborator ID
        //public bool RemoveCollaborator(int NoteId, int UserId, int CollaboratorId);




    }
}
