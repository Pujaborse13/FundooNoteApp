using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entity;

namespace ManagerLayer.Interface
{

    public interface INotesManager 
    {
        public NotesEntity CreateNote(int UserId, NotesModel notesModel);

        public List<NotesEntity> GetAllNotesByUserId(int UserId);

        public List<NotesEntity> FetchNotes(string title, string description);

        public int GetUserNotesCount(int userId);


    }




}
