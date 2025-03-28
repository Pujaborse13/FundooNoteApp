using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;

namespace RepositoryLayer.Interface
{
    public interface INotesRepo
    {

        public NotesEntity CreateNote(int UserId, NotesModel notesModel);

    }
}
