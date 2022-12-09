using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SARAPROJECT.Filters;
using SARAPROJECT.Helpers;
using SARAPROJECT.Models;
using SARAPROJECT.Service;

namespace SARAPROJECT.Controllers
{
    public class VentumsController : Controller
    {
        private readonly SARADBContext _context;
        private authorizeUser objAuthorizeUser = null;

        public VentumsController(SARADBContext context)
        {
            _context = context;
        }

        // GET: Ventums
        [HttpGet]
        public async Task<IActionResult> Index(int pg = 1, int eVent=1)
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Usuario")))
            {
                return RedirectToAction("Login", "Acceso");
            }

            ViewBag.Metodo = new SelectList(_context.MetodoPagos, "IdMetodo", "Nombre");


            //var sARADBContext = _context.Venta.Include(v => v.IdEstventaNavigation).Include(v => v.IdUsuarioNavigation);

            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            if (eVent < 1)
                eVent = 1;

            var data = _context.Venta.Where(v => v.IdEstventa == eVent);
            var pagina = new PAGINA(data.Count(), pg, pageSize);
            ViewBag.pagina = pagina;

            data = data.Skip((pg - 1) * pageSize).Take(pagina.PageSize).Include(v => v.IdEstventaNavigation).Include(v => v.IdUsuarioNavigation).OrderBy(v => v.IdVenta);

            return View(await data.ToListAsync());
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
            ventum.DetalleVenta = _context.DetalleVenta.Where(dv => dv.IdVenta == ventum.IdVenta).Include(dv => dv.IdProductoNavigation).ToArray();
            ventum.DetPagos = _context.DetPagos.Where(dp => dp.IdVenta == ventum.IdVenta).Include(dp=>dp.IdMetodoNavigation).ToArray(); 

            return View(ventum);
        }


        //POST VETIM/InsertVentWithDetails
        [HttpPost]
        public async Task<IActionResult> InsertVentWithDetails([FromBody] Ventum ventum)
        {

            _context.Add(ventum);
            await _context.SaveChangesAsync();
            var vetumActual = _context.Venta.OrderByDescending(a => a.IdVenta).FirstOrDefault();
            return Json(new {respuesta= vetumActual.IdVenta});
        }


        //POST VETUM/preTicket
        [HttpGet]
        public async Task<IActionResult> preTicket(int idVenta)
        {

            var ventum =  await _context.Venta
                // .Include(v => v.IdEstadoNavigation)
                .Include(v => v.IdEstventaNavigation)
                .Include(v => v.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.IdVenta == idVenta);

            if (ventum == null)
            {
                return NotFound();
            }
            
            ventum.DetalleVenta = _context.DetalleVenta.Where(dv => dv.IdVenta == ventum.IdVenta).Include(dv => dv.IdProductoNavigation).ToArray();
            ventum.DetPagos = _context.DetPagos.Where(dp => dp.IdVenta == ventum.IdVenta).Include(dp => dp.IdMetodoNavigation).ToArray();

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

        // GET: Ventums/Register
        public IActionResult Register()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Usuario")))
            {
                return RedirectToAction("Login", "Acceso");
            }
            var str = (HttpContext.Session.GetString("Usuario"));
            var objUsuario = JsonConvert.DeserializeObject<Usuario>(str);
            ViewBag.Usuario = objUsuario.NombreUsuario;
            ViewBag.IdUsuario = objUsuario.IdUsuario;


            // ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "IdEstado");
            //ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            Guid g = Guid.NewGuid();

            /*ViewBags*/
            ViewBag.CodVenta = g.ToString().Substring(0, 9);
            ViewBag.listCategorias = _context.Categoria.ToList();
            ViewBag.NroPedidoValue = _context.Venta.Count() + 1;
            /*ViewData*/
            ViewBag.Metodo = new SelectList(_context.MetodoPagos, "IdMetodo", "Nombre");
            ViewData["IdEstventa"] = new SelectList(_context.EstadoVenta, "IdEstventa", "NombreEstadov");
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "IdMesa", "NombreMesa");

            return View();
        }


        // GET: Ventums/Create
        public IActionResult Create()
        {
            
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Usuario")))
            {
                return RedirectToAction("Login", "Acceso");
            }
            var str = (HttpContext.Session.GetString("Usuario"));
            var objUsuario = JsonConvert.DeserializeObject<Usuario>(str);

            objAuthorizeUser = new authorizeUser(_context);

            if (objAuthorizeUser.OnAuthorization(1, objUsuario.IdRol) == false)
            {
                return NotFound();
            }


            // ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "IdEstado");
            //ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "NombreUsuario");
            Guid g = Guid.NewGuid();

            /*ViewBags*/
            ViewBag.CodVenta = g.ToString().Substring(0, 9); 
            ViewBag.listCategorias = _context.Categoria.ToList();
            ViewBag.NroPedidoValue = _context.Venta.Count() + 1;
            /*ViewData*/
            ViewBag.Metodo = new SelectList(_context.MetodoPagos, "IdMetodo", "Nombre"); 
            ViewData["IdEstventa"] = new SelectList(_context.EstadoVenta, "IdEstventa", "NombreEstadov");
            ViewData["IdMesa"] = new SelectList(_context.Mesas, "IdMesa", "NombreMesa");

            return View();
        }

        /*
        // POST: Ventums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVenta,IdMesa,IdUsuario,IdEstventa,CodVenta,FechaVenta,Total,NroFactura,ClaveAcceso,NroPedido")] Ventum ventum)
        {

            ModelState["IdEstVentaNavigation"].ValidationState = ModelValidationState.Valid;
            ModelState["IdUsuarioNavigation"].ValidationState = ModelValidationState.Valid;

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

            ViewBag.Avatar = HttpContext.Session.GetString("avatarUser");
            return View(ventum);
        }
        */

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
            ventum.DetalleVenta = _context.DetalleVenta.Where(dv => dv.IdVenta == ventum.IdVenta).Include(dv => dv.IdProductoNavigation).ToArray();
            ventum.DetPagos = _context.DetPagos.Where(dp => dp.IdVenta == ventum.IdVenta).Include(dp => dp.IdMetodoNavigation).ToArray();


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
                 ventum.IdEstventa = 3; // el estado de venta 3 es el estado aulado borrado logico
                // _context.Venta.Remove(ventum);
                _context.Venta.Update(ventum);  
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
