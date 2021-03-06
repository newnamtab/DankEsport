using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EsportProject.Models.DBmodels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Logging;

namespace EsportProject.Controllers
{
    public class NewsController : Controller
    {
        private readonly ILogger<NewsController> _logger;

        private readonly NewsContext _context;
        private IHostingEnvironment _environment;
        public NewsController(ILogger<NewsController> logger, NewsContext context, IHostingEnvironment environment)
        {
            _logger = logger;
            _context = context;
            _environment = environment;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Index/News page logged");

            return View(await _context.News.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            _logger.LogInformation("User requested details of news with id " + id);

            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .SingleOrDefaultAsync(m => m.NewsID == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            _logger.LogInformation("News/Create page logged");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsID,Title,subtitle,Content,imgURL,CreateDate")] News news, IFormFile file)
        {
            //Code to upload IMG
            long size = 0;
            var filename = ContentDispositionHeaderValue
                               .Parse(file.ContentDisposition)
                               .FileName
                               .Trim('"').Replace('+','_');
            news.imgURL = $@"\images\" + filename; //Sets the path of the img to the news object
            filename = _environment.WebRootPath + $@"\images\" + filename;
            size += file.Length;
            using (FileStream fs = System.IO.File.Create(filename))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
            _logger.LogInformation("User created news with titel " + news.Title + "and uploaded image with path " + news.imgURL);

            //Code to update DB
            if (ModelState.IsValid)
            {
                _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(news);

        }


        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation("User wants to edit news with id " + id);

            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.SingleOrDefaultAsync(m => m.NewsID == id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NewsID,Title,subtitle,Content,imgURL,CreateDate")] News news)
        {
            if (id != news.NewsID)
            {
                return NotFound();
            }
            _logger.LogInformation("User edited news with id " + id);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.NewsID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            _logger.LogInformation("Delete page logged");

            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .SingleOrDefaultAsync(m => m.NewsID == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _logger.LogInformation("User deleted news with id " + id);

            var news = await _context.News.SingleOrDefaultAsync(m => m.NewsID == id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.NewsID == id);
        }
    }
}
