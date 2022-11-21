using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class ClienteVentum
    {
        public int IdClienvent { get; set; }
        public int IdCliente { get; set; }
        public int IdVenta { get; set; }

        public virtual Cliente IdClienteNavigation { get; set; } = null!;
        public virtual Ventum IdVentaNavigation { get; set; } = null!;
    }
}
