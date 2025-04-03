using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ManagerLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace ManagerLayer.Service
{
   public  class LabelManager : ILabelManager
    {
        private readonly ILabelRepo labelRepo;

        public LabelManager(ILabelRepo labelRepo)
        { 
            this.labelRepo = labelRepo;
        }

        public async Task<LabelEntity> CreateLabelAsync(int userId, string labelName)
        {
            return await labelRepo.CreateLabelAsync(userId, labelName);
 
        }

        public async Task<List<LabelEntity>> GetLabelAsync(int userId)
        { 
            return await labelRepo.GetLabelAsync(userId);
        
        }

        public async Task<bool> AssignLabelToNoteAsync(int noteId, int labelId)
        { 
            return await labelRepo.AssignLabelToNoteAsync(noteId, labelId);
        
        }




    }
}
