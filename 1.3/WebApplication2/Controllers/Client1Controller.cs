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
    public class Client1Controller : Controller
    {
        private readonly DBLibraryContext _context;

        public Client1Controller(DBLibraryContext context)
        {
            _context = context;
        }

        // GET: Client1
        public async Task<IActionResult> Index()
        {
            return View(await _context.Client1.ToListAsync());
        }

        // GET: Client1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client1 = await _context.Client1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client1 == null)
            {
                return NotFound();
            }

            return View(client1);
        }

        // GET: Client1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Client1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Email,Payment,Location")] Client1 client1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(client1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client1);
        }

        // GET: Client1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client1 = await _context.Client1.FindAsync(id);
            if (client1 == null)
            {
                return NotFound();
            }
            return View(client1);
        }

        // POST: Client1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Email,Payment,Location")] Client1 client1)
        {
            if (id != client1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Client1Exists(client1.Id))
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
            return View(client1);
        }

        // GET: Client1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client1 = await _context.Client1
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client1 == null)
            {
                return NotFound();
            }

            return View(client1);
        }

        // POST: Client1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client1 = await _context.Client1.FindAsync(id);
            _context.Client1.Remove(client1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Client1Exists(int id)
        {
            return _context.Client1.Any(e => e.Id == id);
        }
    }
}
