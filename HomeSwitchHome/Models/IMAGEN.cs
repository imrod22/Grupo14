namespace HomeSwitchHome
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IMAGEN")]
    public partial class IMAGEN
    {
        [Key]
        public int IdImagen { get; set; }

        public int IdPropiedad { get; set; }

        [Required]
        public string Path { get; set; }

        public string Nombre { get; set; }

        public virtual PROPIEDAD PROPIEDAD { get; set; }
    }
}
