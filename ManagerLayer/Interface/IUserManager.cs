using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using RepositoryLayer.Entity;

namespace ManagerLayer.Interface
{
    public interface IUserManager
    {
        public UserEntity Register(RegisterModel model);

        public bool CheckEmail(string email);

    }
}
