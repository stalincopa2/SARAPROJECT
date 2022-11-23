using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SARAPROJECT.Models;

namespace SARAPROJECT.Controllers
{
    public class AccesoController : Controller
    {
        private readonly SARADBContext _context;

        public AccesoController(SARADBContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
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
                    string usuario = JsonConvert.SerializeObject(oUser);
                    HttpContext.Session.SetString("Usuario", usuario);
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
