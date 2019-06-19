namespace HomeSwitchHome
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SUBASTA")]
    public partial class SUBASTA
    {
        [Key]
        public int IdSubasta { get; set; }

        public int IdPropiedad { get; set; }

        public int? IdCliente { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaComienzo { get; set; }

        public decimal ValorMinimo { get; set; }

        public decimal ValorActual { get; set; }

        public virtual CLIENTE CLIENTE { get; set; }

        public virtual PROPIEDAD PROPIEDAD { get; set; }

        public string Estado { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaReserva { get; set; }
    }
}
