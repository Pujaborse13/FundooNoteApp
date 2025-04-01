using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
    public class NotesRepo : INotesRepo
    {
        private readonly FundooDBContext context;
        private readonly IConfiguration configuration;


        public NotesRepo(FundooDBContext context, IConfiguration configuration)
        {
            this.context = context; 
            this.configuration = configuration;


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



        public List<NotesEntity> FetchNotes(string title, string description)
        {
            return context.Notes.Where(note => note.Title.Contains(title) && note.Description.Contains(description)).ToList();
        }


        public int GetUserNotesCount(int userId)
        {
            return context.Notes.Count(note => note.UserId == userId);

        }

        public bool DeleteNoteByUserIdAndNoteId(int userId, int noteId)
        {
            var note = context.Notes.FirstOrDefault(n => n.UserId == userId && n.NoteId == noteId);

            if (note != null)
            {
                context.Notes.Remove(note);
                return true;
            }
            return false;
        }



        public NotesEntity UpdateNote(int userId, int noteId, UpdateModel updateModel)
        {
            var note = context.Notes.FirstOrDefault(n => n.NoteId == noteId && n.UserId == userId);

            if (note != null)
            {
                note.Title = updateModel.Title;
                note.Description = updateModel.Description;
                note.Reminder = updateModel.Reminder;
                note.Color = updateModel.Color;
                note.Image = updateModel.Image;
                note.IsArchive = updateModel.IsArchive;
                note.IsPin = updateModel.IsPin;
                note.IsTrash = updateModel.IsTrash;
                note.tUpdatedAt = DateTime.Now;

                context.SaveChanges();
                return note;
            }

            return null;
        }










    }












}



















