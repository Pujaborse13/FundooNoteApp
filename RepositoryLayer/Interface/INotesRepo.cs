﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;

namespace RepositoryLayer.Interface
{
    public interface INotesRepo
    {

        public NotesEntity CreateNote(int UserId, NotesModel notesModel);

        public List<NotesEntity> GetAllNotesByUserId(int UserId);


        //8.Fetch Notes using title and description
        public List<NotesEntity> FetchNotes(string title, string description);


        //9.Return Count of notes a user has
        public int GetUserNotesCount(int userId);

        public bool DeleteNoteByUserIdAndNoteId(int userId, int noteId);

        public NotesEntity UpdateNote(int userId, int noteId, UpdateModel updateModel);






















    }
}
