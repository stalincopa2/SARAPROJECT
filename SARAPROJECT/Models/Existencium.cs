using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class Existencium
    {
        public int IdExistencia { get; set; }
        public int IdProducto { get; set; }
        public int Stock { get; set; }

        public virtual Producto IdProductoNavigation { get; set; } = null!;
    }
}
