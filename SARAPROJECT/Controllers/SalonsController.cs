using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SARAPROJECT.Models;

namespace SARAPROJECT.Controllers
{
    public class SalonsController : Controller
    {
        private readonly SARADBContext _context;

        public SalonsController(SARADBContext context)
        {
            _context = context;
        }

        // GET: Salons
        public async Task<IActionResult> Index()
        {
              return View(await _context.Salons.ToListAsync());
        }

        // GET: Salons/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Salons == null)
            {
                return NotFound();
            }

            var salon = await _context.Salons
                .FirstOrDefaultAsync(m => m.IdSalon == id);
            if (salon == null)
            {
                return NotFound();
            }

            return View(salon);
        }

        // GET: Salons/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Salons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSalon,NombreSalon,Descripcion")] Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(salon);
        }

        // GET: Salons/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Salons == null)
            {
                return NotFound();
            }

            var salon = await _context.Salons.FindAsync(id);
            if (salon == null)
            {
                return NotFound();
            }
            return View(salon);
        }

        // POST: Salons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("IdSalon,NombreSalon,Descripcion")] Salon salon)
        {
            if (id != salon.IdSalon)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalonExists(salon.IdSalon))
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
            return View(salon);
        }

        // GET: Salons/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Salons == null)
            {
                return NotFound();
            }

            var salon = await _context.Salons
                .FirstOrDefaultAsync(m => m.IdSalon == id);
            if (salon == null)
            {
                return NotFound();
            }

            return View(salon);
        }

        // POST: Salons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Salons == null)
            {
                return Problem("Entity set 'SARADBContext.Salons'  is null.");
            }
            var salon = await _context.Salons.FindAsync(id);
            if (salon != null)
            {
                _context.Salons.Remove(salon);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalonExists(short id)
        {
          return _context.Salons.Any(e => e.IdSalon == id);
        }
    }
}
