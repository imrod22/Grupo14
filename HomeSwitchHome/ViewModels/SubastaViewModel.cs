using System;

namespace HomeSwitchHome.ViewModels
{
    public class SubastaViewModel
    {
        public int IdSubasta { get; set; }

        public PropiedadViewModel Propiedad { get; set; }

        public DateTime FechaComienzo { get; set; }

        public decimal ValorMinimo { get; set; }

        public decimal ValorActual { get; set; }

        public CLIENTE Cliente { get; set; }

        public SubastaViewModel() { }

        public SubastaViewModel ToViewModel(SUBASTA subasta)
        {

            this.IdSubasta = subasta.IdSubasta;
            this.Propiedad = new PropiedadViewModel().ToViewModel(subasta.PROPIEDAD);
            this.FechaComienzo = subasta.FechaComienzo;
            this.ValorMinimo = subasta.ValorMinimo;
            this.ValorActual = subasta.ValorActual;

            return this;
        }
    }
}