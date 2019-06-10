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

        public PROPIEDAD Propiedad;

        public int IdCliente;

        public CLIENTE Cliente;

        public string FechaReserva;

        public ReservaViewModel() { }

        public ReservaViewModel ToViewModel(RESERVA reserva)
        {
            this.IdReserva = reserva.IdReserva;
            this.Propiedad =  reserva.PROPIEDAD;
            this.IdPropiedad = this.Propiedad.IdPropiedad;

            this.Cliente = reserva.CLIENTE;
            this.IdCliente = this.Cliente.IdCliente;

            this.FechaReserva = reserva.Fecha.ToString();

            return this;
        }
    }
}