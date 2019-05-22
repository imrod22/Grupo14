namespace HomeSwitchHome
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ADMINISTRADOR")]
    public partial class ADMINISTRADOR
    {
        [Key]
        public int IdAdmin { get; set; }

        public int IdUsuario { get; set; }

        public virtual USUARIO USUARIO { get; set; }
    }
}
