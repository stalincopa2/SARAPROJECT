using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class DetPago
    {
        public int IdDetpago { get; set; }
        public int IdVenta { get; set; }
        public int IdMetodo { get; set; }
        public decimal Monto { get; set; }

        public virtual MetodoPago IdMetodoNavigation { get; set; } = null!;
        public virtual Ventum IdVentaNavigation { get; set; } = null!;
    }
}
