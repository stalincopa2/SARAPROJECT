using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class EstadoVentum
    {
        public EstadoVentum()
        {
            Venta = new HashSet<Ventum>();
        }

        public int IdEstventa { get; set; }
        public string NombreEstadov { get; set; } = null!;
        public string? DescripcionEsv { get; set; }

        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
