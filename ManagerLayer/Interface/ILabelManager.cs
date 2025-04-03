﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;

namespace ManagerLayer.Interface
{
    public interface ILabelManager
    {
        public Task<LabelEntity> CreateLabelAsync(int userId, string labelName);
        public Task<List<LabelEntity>> GetLabelAsync(int userId);
        public Task<bool> AssignLabelToNoteAsync(int noteId, int labelId);

        public Task<bool> RemoveLabelFromNoteAsync(int noteId, int labelId);





    }
}
