namespace HomeSwitchHome
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RESERVA")]
    public partial class RESERVA
    {
        [Key]
        public int IdReserva { get; set; }

        public int IdPropiedad { get; set; }

        public int IdCliente { get; set; }

        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }

        public bool Credito { get; set; }
        
        public virtual CLIENTE CLIENTE { get; set; }

        public virtual PROPIEDAD PROPIEDAD { get; set; }
    }
}
