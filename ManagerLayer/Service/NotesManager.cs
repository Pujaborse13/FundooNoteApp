using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using ManagerLayer.Interface;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

namespace ManagerLayer.Service
{
    public class NotesManager : INotesManager
    {
        private readonly INotesRepo notesRepo;


        public NotesManager(INotesRepo notesRepo)
        { 
            this.notesRepo = notesRepo;
        
        
        }

        public NotesEntity CreateNote(int UserId, NotesModel notesModel)
        {
            return notesRepo.CreateNote(UserId,notesModel);
        
        }


        public List<NotesEntity> GetAllNotesByUserId(int UserId)
        { 
            return notesRepo.GetAllNotesByUserId(UserId);
        
         }


        public List<NotesEntity> FetchNotes(string title, string description)
        { 
            return notesRepo.FetchNotes(title, description);
        
        
        }

        public int GetUserNotesCount(int userId)
        { 
            return notesRepo.GetUserNotesCount(userId);
        }


        public bool DeleteNoteByUserIdAndNoteId(int userId, int noteId)
        { 
            return notesRepo.DeleteNoteByUserIdAndNoteId(userId, noteId);
        
        
        }

        public NotesEntity UpdateNote(int userId, int noteId, UpdateModel updateModel)
        {
            return notesRepo.UpdateNote(userId, noteId, updateModel);
        }


        public int PinNote(int notesId, int userId)
        {
            return notesRepo.PinNote(notesId, userId);

        }

        public int TrashNote(int noteId, int userId)
        {
            return notesRepo.TrashNote(noteId, userId);
        }


        public int ArchiveNote(int noteId, int userId)
        {
            return notesRepo.ArchiveNote(noteId, userId);
        }









    }
}
