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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NewsContext _context;
        private readonly TurnamentContext _Tourcontext;
        private readonly ContactContext _conContext;

        public HomeController(ILogger<HomeController> logger, NewsContext context, ContactContext concontext, TurnamentContext Tcontext)
        {
            _logger = logger;
            _context = context;
            _conContext = concontext;
            _Tourcontext = Tcontext;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Index/Home page logged");
            //TEMPORARY VIEWMODEL ATTEMPT
            Models.IndexViewModel viewModel = new Models.IndexViewModel();

            return View(viewModel);
        }


        public async Task<IActionResult> Teams()
        {
            _logger.LogInformation("Team page logged");
            List<Team> TeamList = await _Tourcontext.Team.ToListAsync();
            return View(TeamList);
        }
        public async Task<IActionResult> Tournaments(int? id)
        {
            await _Tourcontext.Team.ToListAsync();
            List<Turnament> turnamentList = await _Tourcontext.Turnament.ToListAsync();
            List<TeamStanding> TSList = await _Tourcontext.TeamStanding.ToListAsync();
            Models.TournamentViewModel VM;
            if (id == null)
            {
                id = 0;
                VM = new Models.TournamentViewModel(turnamentList[0], turnamentList);
            }
            else
            {
                VM = new Models.TournamentViewModel(turnamentList.Find(x => x.TurnamentID == id), turnamentList);
            }
            _logger.LogInformation("Tournament page logged, ID request: " + id);

            foreach (TeamStanding ts in TSList)
            {
                if (VM.tournament == ts.Turnament)
                {
                    VM.TeamList.Add(new Models.TeamPointsVM(ts));
                }
            }
            List<Models.TeamPointsVM> sortedTeamlist = VM.TeamList.OrderByDescending(o => o.Points).ToList();
            VM.TeamList = sortedTeamlist;
            return View(VM);
        }

        public IActionResult News()
        {
            IEnumerable<News> model = _context.News.ToList() as IEnumerable<News>;
            _logger.LogInformation("News page logged");
            return View(model);
        }
        public async Task<IActionResult> SpecificNews(int? id)
        {
            _logger.LogInformation("SpecificNews page logged, id of news: " + id);
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

            _logger.LogInformation("User send request with mail " + FromAddress);
            return View(tempCon);
        }

        public IActionResult Error()
        {
            _logger.LogInformation("Error page logged");
            _logger.LogError("Error paged reached");
            return View();
        }
        public IActionResult NotFound()
        {
            _logger.LogError("404 page reached");
            return View();
        }
    }
}
