using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SARAPROJECT.Helpers;
using SARAPROJECT.Models;

namespace SARAPROJECT.Controllers
{
    public class AccesoController : Controller
    {
        private readonly SARADBContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public AccesoController(SARADBContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
           
            Sessions sesion = new Sessions(_httpContext);
            try
            {
                    var oUser = (from u in _context.Usuarios
                                where u.SisUsuario == User.Trim() && u.Contracenia == Pass.Trim()
                                select u).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o contracenia Incorrecta";
                        return View();
                    }
                     sesion.sessionUsuarioSet(oUser);                     
                    return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

       
    }
}
