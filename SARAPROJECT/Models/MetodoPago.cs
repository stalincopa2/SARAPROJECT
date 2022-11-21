using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class MetodoPago
    {
        public MetodoPago()
        {
            DetPagos = new HashSet<DetPago>();
        }

        public int IdMetodo { get; set; }
        public string Nombre { get; set; } = null!;
        public string? DetalleMpago { get; set; }

        public virtual ICollection<DetPago> DetPagos { get; set; }
    }
}
