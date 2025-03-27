using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using ManagerLayer.Interface;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace ManagerLayer.Service
{
    public class UserManager : IUserManager

    {
        private readonly IUserRepo userRepo;

        public UserManager(IUserRepo userRepo)
        { 
            this.userRepo = userRepo;

        }

        public UserEntity Register(RegisterModel model)
        { 
            return userRepo.Register(model);
        }


        public bool CheckEmail(string email)
        { 
            return userRepo.CheckEmail(email);
        }

        public string Login(LoginModel model)
        {
            return userRepo.Login(model);
        }
    }
}
