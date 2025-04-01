using System;
using System.Collections.Generic;
using System.Text;
using RepositoryLayer.Entity;

namespace ManagerLayer.Interface
{
    public interface ICollaboratorManager
    {

        //public bool AddCollaborator(int noteId, int userId, string email);

        //Add collaborator to note
        public CollaboratorEntity AddCollaborator(int NoteId, int UserId, string Email);





    }
}
