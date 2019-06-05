using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeSwitchHome.ViewModels
{
    public class ClienteViewModel
    {
        public int IdCliente { get; set; }

        public string Usuario { get; set; }

        public string Password { get; set; }

        public bool Login { get; set; }

        public bool Premium { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public DateTime? FechaDeNacimiento { get; set; }

        public string DomicioFiscal { get; set; }

        public string MedioDePago { get; set; }

        public string Banco { get; set; }

        public int? CBU { get; set; }

        public int? DNI { get; set; }

        public string Email { get; set; }

        public List<RESERVA> Reservas { get; set; }

        public List<NOVEDAD_PROPIEDAD> Notificaciones { get; set; }
        
        public ClienteViewModel() { }

        public ClienteViewModel ToViewModel(CLIENTE cliente)
        {
            this.IdCliente = cliente.IdCliente;
            this.Nombre = cliente.Nombre;
            this.Apellido = cliente.Apellido;
            this.DNI = cliente.DNI;
            this.DomicioFiscal = cliente.DomicioFiscal;
            this.FechaDeNacimiento = cliente.FechaDeNacimiento;
            this.CBU = cliente.CBU;
            this.Banco = cliente.Banco;
            this.MedioDePago = cliente.MedioDePago;
            this.Email = cliente.Email;

            return this;
        }

        public void CargarReservas(List<RESERVA> reservasUsuario)
        {
            this.Reservas = reservasUsuario;
        }

        public void CargarNovedades(List<NOVEDAD_PROPIEDAD> novedades)
        {
            this.Notificaciones = novedades;
        }
    }
}
