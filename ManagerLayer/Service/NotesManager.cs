using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using ManagerLayer.Interface;
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

    }
}
