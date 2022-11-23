using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SARAPROJECT.Controllers;
using SARAPROJECT.Models;

namespace SARAPROJECT.Filters
{
    public class VerificaSession: ActionFilterAttribute
    {
      
        private Usuario oUsuario;
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                base.OnActionExecuted(filterContext);
                if (oUsuario == null)
                {
                    if (filterContext.Controller is AccesoController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Acceso/Login");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Acceso/Login");

            }
            
           
           
        }

    }
}
