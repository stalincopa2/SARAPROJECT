using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class UnidadMedidum
    {
        public UnidadMedidum()
        {
            ProductoUnidads = new HashSet<ProductoUnidad>();
        }

        public int IdUnidad { get; set; }
        public string NombreUnidad { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<ProductoUnidad> ProductoUnidads { get; set; }
    }
}
