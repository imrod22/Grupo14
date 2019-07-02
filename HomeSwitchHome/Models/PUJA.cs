namespace HomeSwitchHome
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PUJA")]
    public partial class PUJA
    {
        [Key]
        public int IdPuja { get; set; }

        public int IdSubasta { get; set; }

        public int IdCliente { get; set; }

        public decimal Monto { get; set; }

        public virtual SUBASTA Subasta { get; set; }

        public virtual CLIENTE Cliente { get; set; }
    }
}