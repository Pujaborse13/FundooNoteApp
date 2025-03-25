using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IUserRepo
    {
        public UserEntity Register(RegisterModel model);

        public bool CheckEmail(string mail);

    }
}
