using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class Stock
    {
        public int IdProducto { get; set; }
        public int? IdStock { get; set; }
        public double? StockActual { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
