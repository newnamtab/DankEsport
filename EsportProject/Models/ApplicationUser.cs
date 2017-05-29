using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;

namespace EsportProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FavoriteTeam { get; set; }
        public string FavoriteGame { get; set; }
    }
}
