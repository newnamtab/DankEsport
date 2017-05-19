using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EsportProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Index/Home page logged");
            return View();
        }

        public IActionResult Teams()
        {
            _logger.LogInformation("Team page logged");
            return View();
        }

        public IActionResult Tournaments()
        {
            _logger.LogInformation("Tournament page logged");
            return View();
        }

        public IActionResult News()
        {
            _logger.LogInformation("News page logged");
            return View();
        }

        public IActionResult Contact()
        {
            _logger.LogInformation("Contact page logged");
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            _logger.LogInformation("Error page logged");
            _logger.LogError("Error paged reached");
            return View();
        }
    }
}
