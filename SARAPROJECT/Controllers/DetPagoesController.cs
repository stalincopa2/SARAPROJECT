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
    public class DetPagoesController : Controller
    {
        private readonly SARADBContext _context;

        public DetPagoesController(SARADBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> InsertPagoAjax([FromBody] DetPago oDetPago)
        {
            _context.Add(oDetPago);
            await _context.SaveChangesAsync();
            
            Ventum oVenta = _context.Venta.Find(oDetPago.IdVenta);
            oVenta.IdEstventa = 2;
            _context.Update(oVenta);
            await _context.SaveChangesAsync();
            

            return Json(new { respuesta = oDetPago.IdVenta});
        }
        // GET: DetPagoes
        public async Task<IActionResult> Index()
        {
            var sARADBContext = _context.DetPagos.Include(d => d.IdMetodoNavigation).Include(d => d.IdVentaNavigation);
            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            return View(await sARADBContext.ToListAsync());
        }

        // GET: DetPagoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DetPagos == null)
            {
                return NotFound();
            }

            var detPago = await _context.DetPagos
                .Include(d => d.IdMetodoNavigation)
                .Include(d => d.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.IdDetpago == id);
            if (detPago == null)
            {
                return NotFound();
            }
            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            return View(detPago);
        }

        // GET: DetPagoes/Create
        public IActionResult Create([FromBody] Ventum ventum)
        {
            ViewData["IdMetodo"] = new SelectList(_context.MetodoPagos, "IdMetodo", "IdMetodo");
            ViewData["IdVenta"] = new SelectList(_context.Venta, "IdVenta", "IdVenta");
            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            return View();
        }

        // POST: DetPagoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetpago,IdVenta,IdMetodo,Monto")] DetPago detPago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detPago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdMetodo"] = new SelectList(_context.MetodoPagos, "IdMetodo", "IdMetodo", detPago.IdMetodo);
            ViewData["IdVenta"] = new SelectList(_context.Venta, "IdVenta", "IdVenta", detPago.IdVenta);
            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            return View(detPago);
        }

        // GET: DetPagoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DetPagos == null)
            {
                return NotFound();
            }

            var detPago = await _context.DetPagos.FindAsync(id);
            if (detPago == null)
            {
                return NotFound();
            }
            ViewData["IdMetodo"] = new SelectList(_context.MetodoPagos, "IdMetodo", "IdMetodo", detPago.IdMetodo);
            ViewData["IdVenta"] = new SelectList(_context.Venta, "IdVenta", "IdVenta", detPago.IdVenta);
            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            return View(detPago);
        }

        // POST: DetPagoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetpago,IdVenta,IdMetodo,Monto")] DetPago detPago)
        {
            if (id != detPago.IdDetpago)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetPagoExists(detPago.IdDetpago))
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
            ViewData["IdMetodo"] = new SelectList(_context.MetodoPagos, "IdMetodo", "IdMetodo", detPago.IdMetodo);
            ViewData["IdVenta"] = new SelectList(_context.Venta, "IdVenta", "IdVenta", detPago.IdVenta);
            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            return View(detPago);
        }

        // GET: DetPagoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DetPagos == null)
            {
                return NotFound();
            }

            var detPago = await _context.DetPagos
                .Include(d => d.IdMetodoNavigation)
                .Include(d => d.IdVentaNavigation)
                .FirstOrDefaultAsync(m => m.IdDetpago == id);
            if (detPago == null)
            {
                return NotFound();
            }

            return View(detPago);
        }

        // POST: DetPagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DetPagos == null)
            {
                return Problem("Entity set 'SARADBContext.DetPagos'  is null.");
            }
            var detPago = await _context.DetPagos.FindAsync(id);
            if (detPago != null)
            {
                _context.DetPagos.Remove(detPago);
            }
            
            await _context.SaveChangesAsync();
            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            return RedirectToAction(nameof(Index));
        }

        private bool DetPagoExists(int id)
        {
            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            return _context.DetPagos.Any(e => e.IdDetpago == id);
        }
    }
}
