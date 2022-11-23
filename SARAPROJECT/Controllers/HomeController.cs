using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SARAPROJECT.Models;
using System.Diagnostics;

namespace SARAPROJECT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Usuario"))){
                return RedirectToAction("Login", "Acceso");
            }
            var str = (HttpContext.Session.GetString("Usuario"));
            var objUsuario = JsonConvert.DeserializeObject<Usuario>(str);
            ViewBag.Usuario = objUsuario.NombreUsuario;
            ViewBag.IdUsuario = objUsuario.IdUsuario;
            return View();
        }

        public IActionResult Logout()
        {
            if (string.IsNullOrWhiteSpace(HttpContext.Session.GetString("Usuario")))
            {     
                return RedirectToAction("Login", "Acceso");
            }
            HttpContext.Session.SetString("Usuario", " ");
            return RedirectToAction("Login", "Acceso");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}