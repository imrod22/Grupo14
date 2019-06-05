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

        public string Estado { get; set; }

        public ClienteViewModel Cliente { get; set; }

        public SubastaViewModel() { }

        public SubastaViewModel ToViewModel(SUBASTA subasta)
        {
            this.IdSubasta = subasta.IdSubasta;
            this.Propiedad = new PropiedadViewModel().ToViewModel(subasta.PROPIEDAD);
            this.FechaComienzo = Convert.ToString(subasta.FechaComienzo.Date);
            this.ValorMinimo = subasta.ValorMinimo;
            this.ValorActual = subasta.ValorActual;
            this.Estado = subasta.Estado;
            
            if(subasta.CLIENTE != null)
                this.Cliente = new ClienteViewModel().ToViewModel(subasta.CLIENTE);

            return this;
        }
    }
}