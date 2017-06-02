using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EsportProject.Models.DBmodels;
using EsportProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EsportProject.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly UserContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(UserContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var vm = new UserManagement
            {
                Users = _dbContext.Users.OrderBy(u => u.Email).Include(u => u.Roles).ToList() 
            };

            return View(vm);
        }
        [HttpGet]
        public async Task<IActionResult> AddRole(string id)
        {
            var user = await GetUserByID(id);
            var vm = new UserManagement
            {
                Rolelist = GetAllRoles(),
                UserID = id,
                Email = user.Email
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(UserManagement UM)
        {
            var ARuser = await GetUserByID(UM.UserID);

            if (ModelState.IsValid)
            {
                var result = await _userManager.AddToRoleAsync(ARuser, UM.NewRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            UM.Rolelist = GetAllRoles(); // Hvis modelstate ikke er valid, så får vi stadig rollen i dropdown. 
            UM.Email = ARuser.Email; // Hvis modelstate ikke er valid, så får vi stadig Emailen ud i view, så man ved hvem der arbejdes på. 
            return View(UM); 

        }

        [HttpGet]
        public async Task<IActionResult> RemoveRole(string id)
        {
            var user = await GetUserByID(id);
            var vm = new UserManagement
            {
                Rolelist = await getUserRoles(user.Id),
                UserID = id,
                Email = user.Email
            };
            return View(vm);

        }

        [HttpPost]
        public async Task<IActionResult> RemoveRole(UserManagement UM)
        {
            var ARuser = await GetUserByID(UM.UserID);

            if (ModelState.IsValid)
            {
                //var result = await _userManager.AddToRoleAsync(ARuser, UM.NewRole);
                var result = await _userManager.RemoveFromRoleAsync(ARuser,UM.NewRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
            }
            UM.Rolelist = GetAllRoles(); // Hvis modelstate ikke er valid, så får vi stadig rollen i dropdown. 
            UM.Email = ARuser.Email; // Hvis modelstate ikke er valid, så får vi stadig Emailen ud i view, så man ved hvem der arbejdes på. 
            return View(UM);

        }

        private async Task<ApplicationUser> GetUserByID(string id) //Helper Mefef til at finde ting på en bruger
        {
            return await _userManager.FindByIdAsync(id);
        }
        private SelectList GetAllRoles() => new SelectList(_roleManager.Roles.OrderBy(r => r.Name));

      
       private async Task<SelectList> getUserRoles(string id)
        {
          var tempuser = _userManager.FindByIdAsync(id);
          ApplicationUser user = await tempuser;

          var temproles =  _userManager.GetRolesAsync(user);
          IList<string> roles = await temproles;
          
          return new SelectList(roles);

        }
    }
}
