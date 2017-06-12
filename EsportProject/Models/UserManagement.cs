using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportProject.Models
{
    public class UserManagement
    {
        public List<ApplicationUser> Users { get; set; }

        public string UserID { get; set; }
        public string NewRole { get; set; }
        public SelectList Rolelist { get; set; }

        public List<RoleListModel> TestRoleList { get; set; }
        public string Email { get; set; }
    }
}
