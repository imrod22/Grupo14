namespace HomeSwitchHome.ViewModels
{
    public class PujaViewModel
    {
        public int IdPuja { get; set; }

        public int IdSubasta { get; set; }

        public int IdCliente { get; set; }

        public decimal Monto { get; set; }

        public PujaViewModel() { }

        public PujaViewModel ToViewModel(PUJA puja)
        {
            this.IdPuja = puja.IdPuja;
            this.IdCliente = puja.IdCliente;
            this.IdSubasta = puja.IdSubasta;
            this.Monto = puja.Monto;

            return this;
        }
    }
}