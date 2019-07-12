using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeSwitchHome.ViewModels
{
    public class ReservaViewModel
    {

        public int IdReserva;

        public int IdPropiedad; 

        public PropiedadViewModel Propiedad;

        public int IdCliente;

        public ClienteViewModel Cliente;

        public string FechaReserva;

        public bool Credito;

        public ReservaViewModel() { }

        public ReservaViewModel ToViewModel(RESERVA reserva)
        {
            this.IdReserva = reserva.IdReserva;

            if (reserva.PROPIEDAD != null)
                this.Propiedad = new PropiedadViewModel().ToViewModel(reserva.PROPIEDAD);
            this.IdPropiedad = this.Propiedad.IdPropiedad;

            if (reserva.CLIENTE != null)
                this.Cliente = new ClienteViewModel().ToViewModel(reserva.CLIENTE);
            this.IdCliente = this.Cliente.IdCliente;

            this.FechaReserva = reserva.Fecha.ToString();
            this.Credito = reserva.Credito;

            return this;
        }
    }
}