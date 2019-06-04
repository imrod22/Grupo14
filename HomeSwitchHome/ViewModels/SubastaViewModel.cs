using System;

namespace HomeSwitchHome.ViewModels
{
    public class SubastaViewModel
    {
        public int IdSubasta { get; set; }

        public PropiedadViewModel Propiedad { get; set; }

        public string FechaComienzo { get; set; }

        public decimal ValorMinimo { get; set; }

        public decimal ValorActual { get; set; }

        public CLIENTE Cliente { get; set; }

        public SubastaViewModel() { }

        public SubastaViewModel ToViewModel(SUBASTA subasta)
        {

            this.IdSubasta = subasta.IdSubasta;
            this.Propiedad = new PropiedadViewModel().ToViewModel(subasta.PROPIEDAD);
            this.FechaComienzo = Convert.ToString(subasta.FechaComienzo.Date);
            this.ValorMinimo = subasta.ValorMinimo;
            this.ValorActual = subasta.ValorActual;

            return this;
        }
    }
}