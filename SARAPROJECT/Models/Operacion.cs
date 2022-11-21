using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class Operacion
    {
        public Operacion()
        {
            RolOperacions = new HashSet<RolOperacion>();
        }

        public int IdOperacion { get; set; }
        public int IdModulo { get; set; }
        public string NombreOperacion { get; set; } = null!;

        public virtual Modulo IdModuloNavigation { get; set; } = null!;
        public virtual ICollection<RolOperacion> RolOperacions { get; set; }
    }
}
