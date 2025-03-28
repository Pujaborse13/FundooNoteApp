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
        public string Login(LoginModel model);  //Authenticates a user and generates a JWT token.

        public ForgotPasswordModel ForgotPassword(string Email);   //Generates a password reset token.

        public bool ResetPassword(string Email, ResetPasswordModel resetPasswordModel);   //Resets the user's password.

    }
}
