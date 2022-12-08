using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using SARAPROJECT.Models;

namespace SARAPROJECT.Filters
{
    public class authorizeUser 
    {

        private readonly SARADBContext _context = new SARADBContext();

        public authorizeUser (SARADBContext context)
        {
            _context = context;
        }

        public bool OnAuthorization(int idOPeracion , int idRol)
        {
            String nombreOperacion = "";
            String nomberModulo = "";

            var listOperaciones = from m in _context.RolOperacions
                                  where m.IdRol == idRol
                                        && m.IdOperacion == idOPeracion
                                  select m;

            if (listOperaciones.ToList().Count() < 1)
            {
                return false;

            }

            return true;
        }
    }
}
