using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class ProductoUnidad
    {
        public int IdProUni { get; set; }
        public int IdProducto { get; set; }
        public int IdUnidad { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual UnidadMedidum IdUnidadNavigation { get; set; } = null!;
    }
}
