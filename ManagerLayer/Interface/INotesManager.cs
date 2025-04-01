﻿using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;

namespace ManagerLayer.Interface
{

    public interface INotesManager 
    {
        public NotesEntity CreateNote(int UserId, NotesModel notesModel);

        public List<NotesEntity> GetAllNotesByUserId(int UserId);

        public List<NotesEntity> FetchNotes(string title, string description);

        public int GetUserNotesCount(int userId);

        public bool DeleteNoteByUserIdAndNoteId(int userId, int noteId);

        public NotesEntity UpdateNote(int userId, int noteId, UpdateModel updateModel);











    }




}
