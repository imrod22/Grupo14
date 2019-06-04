namespace HomeSwitchHome
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CLIENTE")]
    public partial class CLIENTE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CLIENTE()
        {
            NOVEDAD_PROPIEDAD = new HashSet<NOVEDAD_PROPIEDAD>();
            RESERVA = new HashSet<RESERVA>();
            SUBASTA = new HashSet<SUBASTA>();
        }

        [Key]
        public int IdCliente { get; set; }

        public int IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaDeNacimiento { get; set; }

        public string DomicioFiscal { get; set; }

        public string MedioDePago { get; set; }

        public string Banco { get; set; }

        public int? CBU { get; set; }

        public int? DNI { get; set; }

        public string Email { get; set; }

        public virtual USUARIO USUARIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NOVEDAD_PROPIEDAD> NOVEDAD_PROPIEDAD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RESERVA> RESERVA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SUBASTA> SUBASTA { get; set; }
    }
}
