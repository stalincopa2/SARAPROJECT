using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class Bodega
    {
        public Bodega()
        {
            BodegaProductos = new HashSet<BodegaProducto>();
        }

        public int IdBodega { get; set; }
        public string CodBodega { get; set; } = null!;
        public string NombreBodega { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<BodegaProducto> BodegaProductos { get; set; }
    }
}
