using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class Rol
    {
        public Rol()
        {
            RolOperacions = new HashSet<RolOperacion>();
            Usuarios = new HashSet<Usuario>();
        }

        public int IdRol { get; set; }
        public string NombreRol { get; set; } = null!;

        public virtual ICollection<RolOperacion> RolOperacions { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
