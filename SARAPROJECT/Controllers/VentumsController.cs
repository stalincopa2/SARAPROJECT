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
    public class VentumsController : Controller
    {
        private readonly SARADBContext _context;

        public VentumsController(SARADBContext context)
        {
            _context = context;
        }

        // GET: Ventums
        public async Task<IActionResult> Index()
        {
            var sARADBContext = _context.Venta.Include(v => v.IdEstventaNavigation).Include(v => v.IdUsuarioNavigation);

            // var sARADBContext = _context.Venta.Include(v => v.IdEstadoNavigation).Include(v => v.IdEstventaNavigation).Include(v => v.IdUsuarioNavigation);
            return View(await sARADBContext.ToListAsync());
        }

        // GET: Ventums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta
               // .Include(v => v.IdEstadoNavigation)
                .Include(v => v.IdEstventaNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (ventum == null)
            {
                return NotFound();
            }

            return View(ventum);
        }

        //GET: Ventum/productos Ajax

        public IActionResult productosByCategoria(int? id)
        {
            if (id == null)
            {
                id = 1;
            }
            var productosByCategoria = _context.Productos.Where(p => p.IdCategoria == id).ToList();
            return Json(productosByCategoria);
        }

        // GET: Ventums/Create
        public IActionResult Create()
        {
            ViewBag.listCategorias = _context.Categoria.ToList();
            // ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "IdEstado");
            ViewData["IdEstventa"] = new SelectList(_context.EstadoVenta, "IdEstventa", "NombreEstadov");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "IdMesa", "NombreMesa");
            return View();
        }

        // POST: Ventums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenta,IdMesa,IdUsuario,IdEstventa,CodVenta,FechaVenta,Total,NroFactura,ClaveAcceso,NroPedido")] Ventum ventum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "IdEstado", ventum.IdEstado);
            ViewData["IdEstventa"] = new SelectList(_context.EstadoVenta, "IdEstventa", "NombreEstadov", ventum.IdEstventa);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", ventum.IdUsuario);
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "IdMesa", "NombreMesa");
            return View(ventum);
        }

        // GET: Ventums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta.FindAsync(id);
            if (ventum == null)
            {
                return NotFound();
            }
           // ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "IdEstado", ventum.IdEstado);
            ViewData["IdEstventa"] = new SelectList(_context.EstadoVenta, "IdEstventa", "NombreEstadov", ventum.IdEstventa);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", ventum.IdUsuario);
            return View(ventum);
        }

        // POST: Ventums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVenta,IdMesa,IdUsuario,IdEstventa,CodVenta,FechaVenta,Total,NroFactura,ClaveAcceso,NroPedido")] Ventum ventum)
        {
            if (id != ventum.IdVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentumExists(ventum.IdVenta))
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
           // ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "IdEstado", ventum.IdEstado);
            ViewData["IdEstventa"] = new SelectList(_context.EstadoVenta, "IdEstventa", "NombreEstadov", ventum.IdEstventa);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario", ventum.IdUsuario);
            return View(ventum);
        }

        // GET: Ventums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta
                //.Include(v => v.IdEstadoNavigation)
                .Include(v => v.IdEstventaNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == id);
            if (ventum == null)
            {
                return NotFound();
            }

            return View(ventum);
        }

        // POST: Ventums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Venta == null)
            {
                return Problem("Entity set 'SARADBContext.Venta'  is null.");
            }
            var ventum = await _context.Venta.FindAsync(id);
            if (ventum != null)
            {
                _context.Venta.Remove(ventum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentumExists(int id)
        {
          return _context.Venta.Any(e => e.IdVenta == id);
        }
    }
}
