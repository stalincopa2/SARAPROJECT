using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SARAPROJECT.Models
{
    public partial class DetalleVentum
    {
        public int IdDventa { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio{ get; set; }
        public string? Observacion { get; set; }

        [BindNever]
        [ForeignKey("IdProducto")]
        public virtual Producto IdProductoNavigation { get; set; } = null!;
        [BindNever]
        [ForeignKey("IdVenta")]
        public virtual Ventum IdVentaNavigation { get; set; } = null!;
    }
}
