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

        public string Login(LoginModel model);

        public ForgotPasswordModel ForgotPassword(string Email);
        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel);
        
        public List<UserEntity> GetAllUsers();
        public UserEntity GetUserByUserId(int userId);

        public List<UserEntity> GetUserNameStartWithA();
        public int TotalUserCount();

        public List<UserEntity> GetUsersByASCOrder();

        public List<UserEntity> GetUsersByDESCOrder();

        public double GetAverageAgeOfUsers();

        public object OldestAndYoungestUserAge();










    }
}
