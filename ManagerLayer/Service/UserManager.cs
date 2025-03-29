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



        public ForgotPasswordModel ForgotPassword(string Email)
        {

            return userRepo.ForgotPassword(Email);
        }

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        { 
        
            return userRepo.ResetPassword(Email, resetPasswordModel);
        
        }


        public List<UserEntity> GetAllUsers()
        {

            return userRepo.GetAllUsers();

        }


        public UserEntity GetUserByUserId(int userId)
        {
            return userRepo.GetUserByUserId(userId);
        
        }


        public List<UserEntity> GetUserNameStartWithA()
        { 
        
            return userRepo.GetUserNameStartWithA();
        }

        public int TotalUserCount()
        {
            return userRepo.TotalUserCount();
        }

        public List<UserEntity> GetUsersByASCOrder()
        { 
            return userRepo.GetUsersByASCOrder();
        
        
        }


        public List<UserEntity> GetUsersByDESCOrder()
        {
            return userRepo.GetUsersByDESCOrder();
        
        }


        public double GetAverageAgeOfUsers()
        { 
            return userRepo.GetAverageAgeOfUsers();
        
        }


        public object OldestAndYoungestUserAge()
        {
            return userRepo.OldestAndYoungestUserAge();


        }






    }
}
