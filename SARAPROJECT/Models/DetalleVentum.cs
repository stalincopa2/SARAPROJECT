using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class DetalleVentum
    {
        public int IdDventa { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUproduct { get; set; }
        public string? Observacion { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual Ventum IdVentaNavigation { get; set; } = null!;
    }
}
