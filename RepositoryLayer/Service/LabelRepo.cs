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




    }
}
