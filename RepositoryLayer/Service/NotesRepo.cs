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



        public int PinNote(int notesId, int userId)
        {
            NotesEntity notesEntity = context.Notes.FirstOrDefault(note => note.NoteId == notesId && note.UserId == userId);
            if (notesEntity != null)
            {
                if (notesEntity.IsPin)
                {
                    notesEntity.IsPin = false;
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    notesEntity.IsPin = true;
                    context.SaveChanges();
                    return 2;
                }
            }
            else
            {
                return 3;
            }

        }


        public int TrashNote(int noteId, int userId)
        {
            NotesEntity notesEntity = context.Notes.FirstOrDefault(note => note.NoteId == noteId && note.UserId == userId);

            if (notesEntity != null)
            {
                if (notesEntity.IsTrash)
                {
                    notesEntity.IsTrash = false;
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    notesEntity.IsTrash = true;
                    context.SaveChanges();
                    return 2;
                }

            }
            else
            {
                return 3;

            }
        }



        public int ArchiveNote(int noteId, int userId)
        {
            NotesEntity notesEntity = context.Notes.FirstOrDefault(note => note.NoteId == noteId && note.UserId == userId);

            if (notesEntity != null)
            {
                if (notesEntity.IsArchive && notesEntity.IsPin == false)
                {
                    notesEntity.IsArchive = false;
                    context.SaveChanges();
                    return 1;
                }
                else
                {
                    notesEntity.IsTrash = true;
                    context.SaveChanges();
                    return 2;
                }
            }
            else
            {
                return 3;
            }

        }


        public bool AddColourInNote(int noteId, string colour, int userId)
        {
            NotesEntity notesEntity = context.Notes.FirstOrDefault(note => note.NoteId == noteId && note.UserId == userId);
            if (notesEntity != null)
            {
                notesEntity.Color = colour;
                context.SaveChanges();
                return true;
            }

            else
            {
                return false;

            }
        }

        public bool AddReminder(int noteId, int userId, DateTime reminder)
        {
            NotesEntity notesEntity = context.Notes.FirstOrDefault(note => note.NoteId == noteId && note.UserId == userId);

            if (notesEntity != null)
            {
                notesEntity.Reminder = reminder;
                context.SaveChanges();
                return true;
            }

            return false;
        }


        public bool AddImage(int noteId, int userId, IFormFile Image)
        {
            NotesEntity notesEntity = context.Notes.ToList().Find(note => note.NoteId == noteId && note.UserId == userId);
            if (notesEntity != null)
            {
                Account account = new Account(
                configuration["CloudinarySettings:CloudName"],
                configuration["Cloudinary Settings:ApiKey"],
                configuration["CloudinarySettings:ApiSecret"]
                );

                Cloudinary cloudinary = new Cloudinary(account);
                var UploadParameters = new ImageUploadParams()
                {
                    File = new FileDescription(Image.FileName, Image.OpenReadStream()),
                };

                var UploadResult = cloudinary.Upload(UploadParameters);
                string ImagePath = UploadResult.Url.ToString();
                notesEntity.Image = ImagePath;
                context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }







    }  


}



















