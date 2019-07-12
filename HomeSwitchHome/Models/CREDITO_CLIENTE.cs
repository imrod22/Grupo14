namespace HomeSwitchHome
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CREDITO_CLIENTE")]
    public partial class CREDITO_CLIENTE
    {
        [Key]
        public int IdCreditoCliente { get; set; }

        public int IdCliente { get; set; }

        public int Credito { get; set; }

        public int Anio { get; set; }

        public virtual CLIENTE CLIENTE { get; set; }
    }
}