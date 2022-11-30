using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SARAPROJECT.Models
{
    public partial class Producto
    {
        public Producto()
        {
            BodegaProductos = new HashSet<BodegaProducto>();
            DetalleVenta = new HashSet<DetalleVentum>();
            Existencia = new HashSet<Existencium>();
            ProductoUnidads = new HashSet<ProductoUnidad>();
        }

        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }

        [MaxLength(10)]
        public string CodProducto { get; set; } = null!;
        public string NombreProducto { get; set; } = null!;
        [Column(TypeName = "decimal(19, 2)")]
        public decimal? Costo { get; set; }
        [Column(TypeName = "decimal(19, 2)")]
        public decimal Precio { get; set; }
        public int ImpuestoProd { get; set; }
        public string? FotoProducto { get; set; }

        [BindNever]
        [ForeignKey("IdCategoria")]
        public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
        public virtual Stock? Stock { get; set; }
        public virtual ICollection<BodegaProducto> BodegaProductos { get; set; }
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
        public virtual ICollection<Existencium> Existencia { get; set; }
        public virtual ICollection<ProductoUnidad> ProductoUnidads { get; set; }
    }
}
