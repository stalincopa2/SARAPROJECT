using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class Salon
    {
        public Salon()
        {
            Mesas = new HashSet<Mesa>();
        }

        public short IdSalon { get; set; }
        public string NombreSalon { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<Mesa> Mesas { get; set; }
    }
}
