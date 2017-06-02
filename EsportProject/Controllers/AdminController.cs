using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using EsportProject.Models.DBmodels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EsportProject.Controllers
{
    //[Authorize(Roles = "Admin")]
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
            return RedirectToAction("Actions");
        }

        public IActionResult Actions()
        {
            return View();
        }
    }
}