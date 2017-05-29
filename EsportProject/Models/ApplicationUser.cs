using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace EsportProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FavoriteGame { get; set; }
        public string FavoriteTeam { get; set; }
    }
}
