using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EsportProject.Models.DBmodels;
using Microsoft.EntityFrameworkCore;

namespace EsportProject.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserContext _context;
        public AdminController(UserContext context)
        {
            _context = context;

        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User UM)
        {
            List<User> userlist = _context.User.ToList() as List<User>;
            if (ModelState.IsValid)
            {
                foreach (var user in userlist)
                {
                    if (user.Username == UM.Username && user.Password == UM.Password)
                    {
                        return View("Actions", "Admin");
                    }
                }
            }
            return View();
        }

        public IActionResult Actions()
        {
            return View();
        }
    }
}