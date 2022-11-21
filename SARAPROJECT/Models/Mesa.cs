using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SARAPROJECT.Models
{
    public partial class Mesa
    {
        public int IdMesa { get; set; }
        public short IdSalon { get; set; }
        public string CodMesa { get; set; } = null!;
        public string NombreMesa { get; set; } = null!;


        [BindNever]
        [ForeignKey("IdSalon")]
        public virtual Salon IdSalonNavigation { get; set; } = null!;
    }
}
