using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EsportProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
