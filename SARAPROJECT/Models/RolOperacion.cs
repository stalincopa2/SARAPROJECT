using System;
using System.Collections.Generic;

namespace SARAPROJECT.Models
{
    public partial class RolOperacion
    {
        public int IdRope { get; set; }
        public int? IdRol { get; set; }
        public int? IdOperacion { get; set; }

        public virtual Operacion? IdOperacionNavigation { get; set; }
        public virtual Rol? IdRolNavigation { get; set; }
    }
}
