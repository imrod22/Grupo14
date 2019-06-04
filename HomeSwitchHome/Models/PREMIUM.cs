namespace HomeSwitchHome
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PREMIUM")]
    public partial class PREMIUM
    {
        [Key]
        public int IdPremium { get; set; }

        public int IdCliente { get; set; }
    }
}
