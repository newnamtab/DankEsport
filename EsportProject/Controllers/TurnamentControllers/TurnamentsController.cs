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
    public class TurnamentsController : Controller
    {
        private readonly TurnamentContext _context;

        public TurnamentsController(TurnamentContext context)
        {
            _context = context;    
        }

        // GET: Turnaments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Turnament.ToListAsync());
        }

        // GET: Turnaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnament = await _context.Turnament
                .SingleOrDefaultAsync(m => m.TurnamentID == id);
            if (turnament == null)
            {
                return NotFound();
            }

            return View(turnament);
        }

        // GET: Turnaments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Turnaments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TurnamentID,Name,StartDate,Slutdate")] Turnament turnament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(turnament);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(turnament);
        }

        // GET: Turnaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnament = await _context.Turnament.SingleOrDefaultAsync(m => m.TurnamentID == id);
            if (turnament == null)
            {
                return NotFound();
            }
            return View(turnament);
        }

        // POST: Turnaments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TurnamentID,Name,StartDate,Slutdate")] Turnament turnament)
        {
            if (id != turnament.TurnamentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(turnament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TurnamentExists(turnament.TurnamentID))
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
            return View(turnament);
        }

        // GET: Turnaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var turnament = await _context.Turnament
                .SingleOrDefaultAsync(m => m.TurnamentID == id);
            if (turnament == null)
            {
                return NotFound();
            }

            return View(turnament);
        }

        // POST: Turnaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var turnament = await _context.Turnament.SingleOrDefaultAsync(m => m.TurnamentID == id);
            _context.Turnament.Remove(turnament);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TurnamentExists(int id)
        {
            return _context.Turnament.Any(e => e.TurnamentID == id);
        }
    }
}
