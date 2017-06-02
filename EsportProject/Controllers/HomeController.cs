using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MailKit.Net.Smtp;
using MimeKit;
using EsportProject.Models.DBmodels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace EsportProject.Controllers
{
    //[Authorize(Roles = "Admin")]
    //[AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NewsContext _context;
        private readonly ContactContext _conContext;

        public HomeController(ILogger<HomeController> logger, NewsContext context, ContactContext concontext)
        {
            _logger = logger;
            _context = context;
            _conContext = concontext;
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
            IEnumerable<News> model = _context.News.ToList() as IEnumerable<News>;
            _logger.LogInformation("News page logged");
            return View(model);
        }
        public async Task<IActionResult> SpecificNews(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("News", "Home");
            }
            var news = await _context.News.SingleOrDefaultAsync(m => m.NewsID == id);
            if (news == null)
            {
                return RedirectToAction("News", "Home");
            }
            return View(news);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            _logger.LogInformation("Contact page logged");
            ViewData["Message"] = "Your contact page.";

            Models.EmailContact tempCon = new Models.EmailContact();


            return View(tempCon);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Contact(Models.EmailContact contact)
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
            BodyContent = Classes.Sanitizer.SanitizeText(BodyContent);

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
            Models.DBmodels.Contact con = new Contact();
            con.Contactmailadress = contact.EmailAdress;
            con.CreateDate = DateTime.Now;
            con.Message = BodyContent;
            _conContext.Add(con);
            await _conContext.SaveChangesAsync();
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
