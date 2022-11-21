using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class BodegaProducto
    {
        public int IdBodePro { get; set; }
        public int IdBodega { get; set; }
        public int IdProducto { get; set; }

        public virtual Bodega IdBodegaNavigation { get; set; } = null!;
        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
