using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RepositoryLayer.Entity;

namespace ManagerLayer.Interface
{
    public interface ILabelManager
    {
        public Task<LabelEntity> CreateLabelAsync(int userId, string labelName);

    }
}
