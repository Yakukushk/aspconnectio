#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication2;

namespace WebApplication2.Models
{
    public class TourClient1Controller : Controller
    {
        private readonly DBLibraryContext _context;

        public TourClient1Controller(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: TourClient1
        public async Task<IActionResult> Index()
        {
            return View(await _context.TourClient1.ToListAsync());
        }

        // GET: TourClient1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourClient1 = await _context.TourClient1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tourClient1 == null)
            {
                return NotFound();
            }

            return View(tourClient1);
        }

        // GET: TourClient1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TourClient1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email")] TourClient1 tourClient1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tourClient1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tourClient1);
        }

        // GET: TourClient1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourClient1 = await _context.TourClient1.FindAsync(id);
            if (tourClient1 == null)
            {
                return NotFound();
            }
            return View(tourClient1);
        }

        // POST: TourClient1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email")] TourClient1 tourClient1)
        {
            if (id != tourClient1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourClient1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourClient1Exists(tourClient1.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tourClient1);
        }

        // GET: TourClient1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tourClient1 = await _context.TourClient1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tourClient1 == null)
            {
                return NotFound();
            }

            return View(tourClient1);
        }

        // POST: TourClient1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tourClient1 = await _context.TourClient1.FindAsync(id);
            _context.TourClient1.Remove(tourClient1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourClient1Exists(int id)
        {
            return _context.TourClient1.Any(e => e.Id == id);
        }
    }
}
