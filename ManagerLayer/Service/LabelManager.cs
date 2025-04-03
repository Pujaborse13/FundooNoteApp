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

        public Task<LabelEntity> CreateLabelAsync(int userId, string labelName)
        {
            return labelRepo.CreateLabelAsync(userId, labelName);
 
        }
    }
}
