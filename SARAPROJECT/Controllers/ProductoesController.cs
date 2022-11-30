using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SARAPROJECT.Models;
using SARAPROJECT.Service;

namespace SARAPROJECT.Controllers
{
    public class ProductoesController : Controller
    {
        private readonly SARADBContext _context;
        private PictureService pService = new PictureService();
        public ProductoesController(SARADBContext context)
        {
            _context = context;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            var sARADBContext = _context.Productos.Include(p => p.IdCategoriaNavigation);
            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            return View(await sARADBContext.ToListAsync());
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nombre");
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,IdCategoria,CodProducto,NombreProducto,Costo,Precio,ImpuestoProd")] Producto producto, IFormFile ProducImgFile )
        {
            
            ModelState["IdCategoriaNavigation"].ValidationState = ModelValidationState.Valid;
            ModelState["ProducImgFile"].ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                if (ProducImgFile != null)
                {
                    producto.FotoProducto = pService.Insert(ProducImgFile);
                }
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nombre", producto.IdCategoria);
            return View(producto);
        }

        // GET: Productoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nombre", producto.IdCategoria);
            return View(producto);
        }

        // POST: Productoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProducto,IdCategoria,CodProducto,NombreProducto,Costo,Precio,ImpuestoProd,FotoProducto")] Producto producto, IFormFile ProducImgFile)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }
            ModelState["IdCategoriaNavigation"].ValidationState = ModelValidationState.Valid;
            ModelState["ProducImgFile"].ValidationState = ModelValidationState.Valid;

            if (ModelState.IsValid)
            {
                try
                {
                    if (ProducImgFile!= null)
                    {
                        if (producto.FotoProducto != null)
                        {
                            pService.Delete(producto.FotoProducto);
                        }
 
                        producto.FotoProducto = pService.Insert(ProducImgFile);
                    }

                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
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
            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "IdCategoria", "Nombre", producto.IdCategoria);
            return View(producto);
        }

        // GET: Productoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'SARADBContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                if (producto.FotoProducto != null)
                {
                    pService.Delete(producto.FotoProducto);
                }
                _context.Productos.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return _context.Productos.Any(e => e.IdProducto == id);
        }
    }
}
