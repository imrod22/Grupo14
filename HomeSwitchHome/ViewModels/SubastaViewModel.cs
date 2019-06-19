using System;

namespace HomeSwitchHome.ViewModels
{
    public class SubastaViewModel
    {
        public int IdSubasta { get; set; }

        public int IdPropiedad { get; set; }

        public PropiedadViewModel Propiedad { get; set; }

        public string FechaComienzo { get; set; }

        public string FechaReserva { get; set; }

        public decimal ValorMinimo { get; set; }

        public decimal ValorActual { get; set; }

        public string Estado { get; set; }

        public int? IdCliente { get; set; }

        public ClienteViewModel Cliente { get; set; }

        public SubastaViewModel() { }

        public SubastaViewModel ToViewModel(SUBASTA subasta)
        {
            this.IdSubasta = subasta.IdSubasta;
            this.IdPropiedad = subasta.IdPropiedad;

            if (subasta.PROPIEDAD != null)
                this.Propiedad = new PropiedadViewModel().ToViewModel(subasta.PROPIEDAD);
            this.FechaComienzo = Convert.ToString(subasta.FechaComienzo.Date);
            this.FechaReserva = Convert.ToString(subasta.FechaReserva.Date);
            this.ValorMinimo = subasta.ValorMinimo;
            this.ValorActual = subasta.ValorActual;
            this.Estado = subasta.Estado;
            this.IdCliente = subasta.IdCliente;
            
            if(subasta.CLIENTE != null)
                this.Cliente = new ClienteViewModel().ToViewModel(subasta.CLIENTE);

            return this;
        }
    }
}