using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class Sucursal
    {
        public Sucursal()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdSucursal { get; set; }
        public string Ruc { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
