using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EsportProject.Models.DBmodels;


namespace EsportProject.Controllers.TurnamentControllers
{
    public class TeamStandingsController : Controller
    {
        private readonly TurnamentContext _context;
        public TeamStandingsController(TurnamentContext context)
        {
            _context = context;
        }

        // GET: TeamStandings
        public async Task<IActionResult> Index()
        {
            return View(await _context.TeamStanding.ToListAsync());
        }

        // GET: TeamStandings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamStanding = await _context.TeamStanding
                .SingleOrDefaultAsync(m => m.TeamStandingID == id);
            if (teamStanding == null)
            {
                return NotFound();
            }

            return View(teamStanding);
        }

        // GET: TeamStandings/Create
        public IActionResult Create()
        {
            List<Team> teamList = _context.Team
                                    .FromSql("SELECT * FROM Team")
                                    .ToList();
            List<Turnament> TurnamentList = _context.Turnament
                                    .FromSql("SELECT * FROM Turnament")
                                    .ToList();
            ViewData["teams"] = teamList;
            ViewData["turnament"] = TurnamentList;
            return View();
        }

        // POST: TeamStandings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeamStandingID,Turnament,Team,WonMatches,drawMatches,LostMatches")] TeamStanding teamStanding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teamStanding);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(teamStanding);
        }

        // GET: TeamStandings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamStanding = await _context.TeamStanding.SingleOrDefaultAsync(m => m.TeamStandingID == id);
            if (teamStanding == null)
            {
                return NotFound();
            }
            return View(teamStanding);
        }

        // POST: TeamStandings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeamStandingID,WonMatches,drawMatches,LostMatches")] TeamStanding teamStanding)
        {
            if (id != teamStanding.TeamStandingID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teamStanding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamStandingExists(teamStanding.TeamStandingID))
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
            return View(teamStanding);
        }

        // GET: TeamStandings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teamStanding = await _context.TeamStanding
                .SingleOrDefaultAsync(m => m.TeamStandingID == id);
            if (teamStanding == null)
            {
                return NotFound();
            }

            return View(teamStanding);
        }

        // POST: TeamStandings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teamStanding = await _context.TeamStanding.SingleOrDefaultAsync(m => m.TeamStandingID == id);
            _context.TeamStanding.Remove(teamStanding);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TeamStandingExists(int id)
        {
            return _context.TeamStanding.Any(e => e.TeamStandingID == id);
        }
    }
}
