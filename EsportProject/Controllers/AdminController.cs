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
        public IActionResult Index()
        {

            var Userlist = _context.User.FromSql("SELECT * FROM User").ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Login(User UM)
        {
            if (ModelState.IsValid)
            {

            }
            return View(UM);
        }

        public IActionResult Actions()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {

            }
            return View();
        }
    }
}