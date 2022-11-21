using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SARAPROJECT.Models
{
    public partial class Categorium
    {
        public Categorium()
        {
            Productos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        [Display(Name = "CATEGORIA")]
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
