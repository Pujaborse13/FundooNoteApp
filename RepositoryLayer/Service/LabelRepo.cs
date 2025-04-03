using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace RepositoryLayer.Service
{
    public class LabelRepo : ILabelRepo
    {
        private readonly FundooDBContext context;

        public LabelRepo(FundooDBContext context)
        {
            this.context = context;

        }


        public async Task<LabelEntity> CreateLabelAsync(int userId, string labelName)
        {
            var result  = context.Notes.FirstOrDefault(n =>n.UserId == userId);

            if (result != null)
            {
                LabelEntity label = new LabelEntity
                {
                    LabelName = labelName,
                    UserId = userId
                };
                context.Labels.Add(label);
                await context.SaveChangesAsync();
                return label;
            }
            else { 
                
                return null; 
            }
        }


        public async Task<List<LabelEntity>> GetLabelAsync(int userId)
        { 

            return await context.Labels.Where(l => l.UserId == userId).ToListAsync();
        }




        public async Task<bool> AssignLabelToNoteAsync(int noteId, int labelId)
        {
            var note = await context.Notes.FindAsync(noteId);
            var label = await context.Labels.FindAsync(labelId);

            if (note == null || label == null)
            {
                return false;
            }
            else
            {
                context.NoteLabels.Add(new NoteLabelEntity { NoteId = noteId, LabelId = labelId });
                await context.SaveChangesAsync();
                return true;
            }

        }


        public async Task<bool> RemoveLabelFromNoteAsync(int noteId, int labelId)
        {
            var noteLabel = await context.NoteLabels.FindAsync(noteId, labelId);
            if (noteLabel == null)
            {
                return false;

            }
            context.NoteLabels.Remove(noteLabel);
            await context.SaveChangesAsync();
            return true;


        }
       
    









    }
}
