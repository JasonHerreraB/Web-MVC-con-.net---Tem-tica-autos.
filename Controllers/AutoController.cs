using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using guia_2.Models;
using Microsoft.AspNetCore.Authorization;

namespace guia_2.Controllers
{
    public class AutoController : Controller
    {
        private readonly AppDbContext _context;

        public AutoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Auto
        public async Task<IActionResult> Index()
        {
            return _context.Autos != null ?
                        View(await _context.Autos.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Autos'  is null.");
        }

        // GET: Auto/Details/5
        [Authorize(Roles = "ADMIN,DETAIL")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Autos == null)
            {
                return NotFound();
            }

            var autos = await _context.Autos.Where(c => c.Id == id)
                .Include(c => c.Images).FirstAsync();
            if (autos == null)
            {
                return NotFound();
            }

            return View(autos);
        }

        // GET: Auto/Create
        [Authorize(Roles = "ADMIN,CREATE")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Auto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Year,Origin,Weight,Acceleration,Poster,Description")] Autos autos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(autos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autos);
        }

        // GET: Auto/Edit/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Autos == null)
            {
                return NotFound();
            }

            var autos = await _context.Autos.FindAsync(id);
            if (autos == null)
            {
                return NotFound();
            }
            return View(autos);
        }

        // POST: Auto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Year,Origin,Weight,Acceleration,Poster,Description")] Autos autos)
        {
            if (id != autos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutosExists(autos.Id))
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
            return View(autos);
        }

        // GET: Auto/Delete/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Autos == null)
            {
                return NotFound();
            }

            var autos = await _context.Autos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autos == null)
            {
                return NotFound();
            }

            return View(autos);
        }

        // POST: Auto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Autos == null)
            {
                return Problem("Entity set 'AppDbContext.Autos'  is null.");
            }
            var autos = await _context.Autos.FindAsync(id);
            if (autos != null)
            {
                _context.Autos.Remove(autos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Search(string search)
        {

            if (!string.IsNullOrEmpty(search))
            {
                List<Autos> autos = await _context.Autos
                    .Where(c => (c.Name != null && c.Name.Contains(search))).ToListAsync();
                return View("Index", autos);
            }


            // Redirige a la vista "Index"
            return RedirectToAction("Index");
        }

        private bool AutosExists(int id)
        {
            return (_context.Autos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
