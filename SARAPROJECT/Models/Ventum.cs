using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SARAPROJECT.Models
{
    public partial class Ventum
    {
        public Ventum()
        {
            ClienteVenta = new HashSet<ClienteVentum>();
            DetPagos = new HashSet<DetPago>();
            DetalleVenta = new HashSet<DetalleVentum>();
        }

        public int IdVenta { get; set; }
        [DisplayName("Mesa")]
        public int? IdMesa { get; set; }
        [DisplayName("Usuario")]
        public int IdUsuario { get; set; }
        [DisplayName("Estado")]
        public int IdEstventa { get; set; }
       //  public int IdEstado { get; set; }
       //public int IdMetodo { get; set; }
       // public strIdUsuario ing Nombre { get; set; } = null!;
        public string CodVenta { get; set; } = null!;
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public string? NroFactura { get; set; }
        public string? ClaveAcceso { get; set; }
        public int? NroPedido { get; set; }

        //  public virtual Estado IdEstadoNavigation { get; set; } = null!;
        [DisplayName("Estado")]
        [BindNever]
        [ForeignKey("IdEstventa")]
        public virtual EstadoVentum IdEstventaNavigation { get; set; } = null!;
        [DisplayName("Usuario")]
        [BindNever]
        [ForeignKey("IdUsuario")]
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

        public virtual ICollection<ClienteVentum> ClienteVenta { get; set; }
        public virtual ICollection<DetPago> DetPagos { get; set; }
        public virtual ICollection<DetalleVentum> DetalleVenta { get; set; }
    }
}
