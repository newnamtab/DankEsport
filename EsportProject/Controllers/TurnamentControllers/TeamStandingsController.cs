using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EsportProject.Models.DBmodels;
using EsportProject.Models;

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
            await _context.Team.ToListAsync();
            await _context.Turnament.ToListAsync();
            IEnumerable<TeamStanding> tsList = await _context.TeamStanding.ToListAsync();
            IEnumerable<TeamStanding> sortedList = tsList.OrderBy(t => t.Turnament.Name);
            return View(sortedList);
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
            TeamTurnamentVM VM = new TeamTurnamentVM(_context);
            return View(VM);
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
        [HttpPost]
        public IActionResult NewCreate()
        {
            Team team = GetTeamFromID(int.Parse(Request.Form["Team"]));
            Turnament tournament = GetTournamentFromID(int.Parse(Request.Form["Turnament"]));
            TeamStanding teamStanding = new TeamStanding();
            if (team != null && tournament != null)
            {
                teamStanding.Team = team;
                teamStanding.Turnament = tournament;
                teamStanding.WonMatches = 0;
                teamStanding.drawMatches = 0;
                teamStanding.LostMatches = 0;
                _context.Add(teamStanding);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }
        private Team GetTeamFromID(int ID)
        {
            List<Team> teamlist = _context.Team.ToList() as List<Team>;
            foreach (Team tm in teamlist)
            {
                if (tm.TeamID == ID)
                {
                    return tm;
                }
            }
            return null;
        }
        private Turnament GetTournamentFromID(int ID)
        {
            List<Turnament> TurnList = _context.Turnament.ToList() as List<Turnament>;
            foreach (Turnament tur in TurnList)
            {
                if (tur.TurnamentID == ID)
                {
                    return tur;
                }
            }
            return null;
        }
    }
}
