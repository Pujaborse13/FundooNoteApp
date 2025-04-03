using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface ILabelRepo
    {
        public Task<LabelEntity> CreateLabelAsync(int userId, string labelName);


    }
}
