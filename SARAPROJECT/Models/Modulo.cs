using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class Modulo
    {
        public Modulo()
        {
            Operacions = new HashSet<Operacion>();
        }

        public int IdModulo { get; set; }
        public string NombreModulo { get; set; } = null!;

        public virtual ICollection<Operacion> Operacions { get; set; }
    }
}
