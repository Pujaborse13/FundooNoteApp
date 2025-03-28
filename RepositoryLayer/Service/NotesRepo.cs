using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class NotesRepo : INotesRepo
    {
        private readonly FundooDBContext context;


        public NotesRepo(FundooDBContext context)
        {
            this.context = context; ;

        }
         public NotesEntity CreateNote(int UserId, NotesModel notesModel)

        { 
            NotesEntity notesEntity = new NotesEntity();
            notesEntity.Title = notesModel.Title;
            notesEntity.Description = notesModel.Description;
            notesEntity.CreatedAt = DateTime.Now;
            notesEntity.tUpdatedAt = DateTime.Now;

            
            notesEntity.UserId = UserId;
            context.Notes.Add(notesEntity);
            context.SaveChanges();
            return notesEntity;

        
        
        }


        public List<NotesEntity> GetAllNotesByUserId(int UserId)
        {
            return context.Notes.Where(note => note.UserId == UserId).ToList();
        }



    }
}
