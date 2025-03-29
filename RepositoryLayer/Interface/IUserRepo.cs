using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;
using Microsoft.AspNetCore.Routing.Constraints;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
    public interface IUserRepo
    {
        public UserEntity Register(RegisterModel model);
        public bool CheckEmail(string mail);
        public string Login(LoginModel model);  //Authenticates a user and generates a JWT token.

        public ForgotPasswordModel ForgotPassword(string Email);   //Generates a password reset token.

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel);   //Resets the user's password.

        //***********************************

        //Review Task

        //1.get all Users
       public List<UserEntity>GetAllUsers();

        //2.Find a user by ID
        public UserEntity GetUserByUserId(int userId);


        //3.Get users whose name starts with 'A'
        public List<UserEntity> GetUserNameStartWithA();

        //4.Count the total number of users
        public int TotalUserCount();


        //5.Get users ordered by name (ascending & descending)
        public List<UserEntity> GetUsersByASCOrder();

        public List<UserEntity> GetUsersByDESCOrder();


        //6.Get the average age of users
        public double GetAverageAgeOfUsers();


        //7.Get the oldest and youngest user age
        public object OldestAndYoungestUserAge();













    }
}   
