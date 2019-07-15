namespace HomeSwitchHome.ViewModels
{
    public class HotSaleViewModel
    {
        public int IdHotSale { get; set; }

        public int IdPropiedad { get; set; }

        public string FechaDisponible { get; set; }

        public decimal Precio { get; set; }

        public bool Estado { get; set; }

        public PropiedadViewModel Propiedad { get; set; }

        public HotSaleViewModel() { }

        public HotSaleViewModel ToViewModel(HOTSALE hotsale)
        {
            this.IdHotSale = hotsale.IdHotSale;
            this.IdPropiedad = hotsale.IdPropiedad;
            this.FechaDisponible = hotsale.FechaDisponible.ToString();
            this.Precio = hotsale.Precio;
            this.Estado = hotsale.Estado;
            this.Propiedad = new PropiedadViewModel().ToViewModel(hotsale.PROPIEDAD);

            return this;
        }
    }
}