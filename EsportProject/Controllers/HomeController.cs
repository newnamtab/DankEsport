using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MailKit.Net.Smtp;
using MimeKit;


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
            //TEMPORARY VIEWMODEL ATTEMPT
            Models.IndexViewModel viewModel = new Models.IndexViewModel();

            return View(viewModel);
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

        [HttpGet]
        public IActionResult Contact()
        {
            _logger.LogInformation("Contact page logged");
            ViewData["Message"] = "Your contact page.";

            Models.EmailContact tempCon = new Models.EmailContact();


            return View(tempCon);
        }

        [HttpPost]
        public IActionResult Contact(Models.EmailContact contact)
        {
            Models.EmailContact tempCon = new Models.EmailContact();


            //From Address
            string FromAddress = "mail@newnamtab.dk";
            string FromAdressTitle = "Dank E-Sport";
            //To Address
            string ToAddress = "newnamtab@yahoo.dk";  //OR DEFAULT MAIL ADRESS
            string ToAdressTitle = "Microsoft ASP.NET Core";
            string Subject = "New Contact";
            string BodyContent = contact.FirstName + " " + contact.LastName + Environment.NewLine + contact.EmailAdress + " har indgivet en henvendelse:" + Environment.NewLine + contact.Message;

            //Smtp Server
            string SmtpServer = "send.one.com";
            //Smtp Port Number
            int SmtpPortNumber = 587;

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
            mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
            mimeMessage.Subject = Subject;
            mimeMessage.Body = new TextPart("plain")
            {
                Text = BodyContent

            };

            using (var client = new SmtpClient())
            {

                client.Connect(SmtpServer, SmtpPortNumber, false);
                // Note: only needed if the SMTP server requires authentication
                // Error 5.5.1 Authentication 
                client.Authenticate("mail@newnamtab.dk", "mailpassword");
                client.Send(mimeMessage);
                client.Disconnect(true);
                tempCon.Info = "The mail was sent succesfully";

            }
            ModelState.Clear();

            return View(tempCon);
        }

        public IActionResult Error()
        {
            _logger.LogInformation("Error page logged");
            _logger.LogError("Error paged reached");
            return View();
        }
    }
}
