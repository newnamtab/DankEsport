using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EsportProject.Models
{
    public class Register
    {
        [Required, EmailAddress, MaxLength(256), Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required, MinLength(6),MaxLength(50),DataType(DataType.Password),Display(Name = "Password")]
        public string Password { get; set; }
        [Required, MinLength(6), MaxLength(50), DataType(DataType.Password), Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password does not match. Crone skal være en daddy")]
        public string ConfirmPassword { get; set; }

    }
}
