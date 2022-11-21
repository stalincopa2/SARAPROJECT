using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SARAPROJECT.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Venta = new HashSet<Ventum>();
        }

        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public int IdSucursal { get; set; }
        public string CedulaUsuario { get; set; } = null!;
        public string NombreUsuario { get; set; } = null!;
        public string? EmailUsuario { get; set; }
        public string Sexo { get; set; } = null!;
        public string Contracenia { get; set; } = null!;
        public string SisUsuario { get; set; } = null!;

        [BindNever]
        [ForeignKey("IdRol")]
        public virtual Rol IdRolNavigation { get; set; } = null!;

        [BindNever]
        [ForeignKey("IdSucursal")]
        public virtual Sucursal IdSucursalNavigation { get; set; } = null!;
        public virtual ICollection<Ventum> Venta { get; set; }
    }
}
