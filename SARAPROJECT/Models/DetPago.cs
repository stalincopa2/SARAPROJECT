using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SARAPROJECT.Models
{
    public partial class DetPago
    {
        public int IdDetpago { get; set; }
        public int IdVenta { get; set; }
        public int IdMetodo { get; set; }
        public decimal Monto { get; set; }


        [BindNever]
        [ForeignKey("IdMetodo")]
        public virtual MetodoPago IdMetodoNavigation { get; set; } = null!;


        [BindNever]
        [ForeignKey("IdVenta")]
        public virtual Ventum IdVentaNavigation { get; set; } = null!;
    }
}
