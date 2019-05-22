namespace HomeSwitchHome
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NOVEDAD_PROPIEDAD
    {
        [Key]
        public int IdNovedadPropiedad { get; set; }

        public int IdPropiedad { get; set; }

        public int IdCliente { get; set; }

        public virtual CLIENTE CLIENTE { get; set; }

        public virtual PROPIEDAD PROPIEDAD { get; set; }
    }
}
