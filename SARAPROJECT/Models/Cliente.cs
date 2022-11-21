using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            ClienteVenta = new HashSet<ClienteVentum>();
        }

        public int IdCliente { get; set; }
        public string NombreCliente { get; set; } = null!;
        public string RucCliente { get; set; } = null!;
        public string? EmailCliente { get; set; }
        public string? TelefonoCliente { get; set; }
        public string? DireccionCliente { get; set; }

        public virtual ICollection<ClienteVentum> ClienteVenta { get; set; }
    }
}
