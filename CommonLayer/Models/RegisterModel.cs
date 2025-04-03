using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class RegisterModel
    {
        [StringLength(20,MinimumLength =3, ErrorMessage ="Provide input minimum 3 letters")]
        public string FirstName { get; set; }
        
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Provide input minimum 3 letters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DOB { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Please enter valid Email ID")]        
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
