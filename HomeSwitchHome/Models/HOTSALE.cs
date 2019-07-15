namespace HomeSwitchHome
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HOTSALE")]
    public partial class HOTSALE
    {
        [Key]
        public int IdHotSale { get; set; }

        public int IdPropiedad { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaDisponible { get; set; }

        public decimal Precio { get; set; }

        public bool Estado { get; set; }

        public virtual PROPIEDAD PROPIEDAD { get; set; }
    }
}
