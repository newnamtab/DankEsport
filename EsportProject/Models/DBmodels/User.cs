using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EsportProject.Models.DBmodels
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<NewsContext> options)
            : base(options)
        { }
        public DbSet<User> User { get; set; }
    }
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
