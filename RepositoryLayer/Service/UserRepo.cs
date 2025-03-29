using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;

namespace RepositoryLayer.Service
{
    public class UserRepo : IUserRepo
    {
        private readonly FundooDBContext context;
        private readonly IConfiguration configuration;

        public UserRepo(FundooDBContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;  //Injects IConfiguration (to access settings like JWT keys from appsettings.json).

        }

        public UserEntity Register(RegisterModel model)
        {
            UserEntity user = new UserEntity();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.DOB = model.DOB;
            user.Gender = model.Gender;
            user.Email = model.Email;
            user.Password = EncodePasswordToBase64(model.Password); //Encoding the password into Base64 format for storage
            
            this.context.Users.Add(user);
            context.SaveChanges();
            return user;

        }

        public bool CheckEmail(string email)
        {
            var result = this.context.Users.FirstOrDefault(x => x.Email == email);
            if (result == null)
            {
                return false;
            }

            return true;
        }


        private string EncodePasswordToBase64(string password)
        {
            try
            {  
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);

            }


        }



        public string Login(LoginModel model)
        {
            var checkUser = context.Users.FirstOrDefault(x => x.Email == model.Email && x.Password == EncodePasswordToBase64(model.Password));
            if (checkUser != null)
            {
                var token = GenerateToken(checkUser.Email, checkUser.UserId);
                return token;
            }
            return null;
        }



        // To generate token
        private string GenerateToken(string email , int userId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("EmailID",email),
                new Claim("UserID",userId.ToString())

            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }



        public ForgotPasswordModel ForgotPassword(string Email)
        { 
            UserEntity user = context.Users.ToList().Find(user => user.Email == Email);
            ForgotPasswordModel forgotPassword = new ForgotPasswordModel();
            forgotPassword.Email = user.Email;
            forgotPassword.UserID= user.UserId;
            forgotPassword.Token = GenerateToken(user.Email, user.UserId);

            return forgotPassword;

        
        }



        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel)
        {
            UserEntity User = context.Users.ToList().Find(user => user.Email == Email);

            if (CheckEmail(User.Email))
            {

                User.Password = EncodePasswordToBase64(resetPasswordModel.ConfirmPassword);
                //User.ChangedAt = DateTime.Now;
                context.SaveChanges();
                return true;
            }

            else
            {
                return false;

            }

        }

        //---------------------------------------
        //1.Get all users
        public List<UserEntity> GetAllUsers()
        {
            return context.Users.ToList();

        }


        public UserEntity GetUserByUserId(int userId)
        {
            return context.Users.FirstOrDefault(user => user.UserId == userId);

        }


        public List<UserEntity> GetUserNameStartWithA()
        { 
            return context.Users.Where(user => user.FirstName.StartsWith("A")).ToList();        
        }


        public int TotalUserCount() 
        {
            return context.Users.Count();
        }


        public List<UserEntity> GetUsersByASCOrder()
        {

            return context.Users.OrderBy(user => user.FirstName).ToList();

        }



        public List<UserEntity> GetUsersByDESCOrder()
        {
            return context.Users.OrderByDescending(user => user.FirstName).ToList();
        }

        public double GetAverageAgeOfUsers()
        {
           double averageAge = context.Users
                      .Select(user => DateTime.Now.Year - user.DOB.Year - (DateTime.Now.DayOfYear < user.DOB.DayOfYear ? 1 : 0))
                      .Average();
            
            return averageAge;



        }

        public object OldestAndYoungestUserAge()
        {
            int oldestAge = context.Users
                .Select(user => DateTime.Now.Year - user.DOB.Year - (DateTime.Now.DayOfYear < user.DOB.DayOfYear ? 1 : 0))
                .Max();

            int youngestAge = context.Users
                .Select(user => DateTime.Now.Year - user.DOB.Year - (DateTime.Now.DayOfYear < user.DOB.DayOfYear ? 1 : 0))
                .Min();

            return new { OldestAge = oldestAge, YoungestAge = youngestAge };
        }






    





    }
}