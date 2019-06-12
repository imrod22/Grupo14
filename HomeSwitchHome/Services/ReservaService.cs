using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeSwitchHome.ViewModels;

namespace HomeSwitchHome.Services
{
    public class ReservaService : IReservaService
    {
        private HomeSwitchHomeDB HomeSwitchDB;

        public ReservaService()
        {
            this.HomeSwitchDB = new HomeSwitchHomeDB();
        }

        public string AgregarReserva(ReservaViewModel reservaModelo)
        {
            var clienteCreditos = this.ObtenerReservasCliente(reservaModelo.IdCliente);
            var propiedadReservas = this.ObtenerReservasPropiedad(reservaModelo.IdPropiedad);
            var servicioSubasta = new SubastaService();

            var propiedadSubastas = servicioSubasta.ObtenerSubastasDePropiedad(reservaModelo.IdPropiedad).Where(t => Convert.ToDateTime(reservaModelo.FechaReserva).CompareTo(Convert.ToDateTime(t.FechaComienzo)) >= 0
                                && Convert.ToDateTime(reservaModelo.FechaReserva).CompareTo(Convert.ToDateTime(t.FechaComienzo).AddDays(10)) <= 0);

            if (clienteCreditos.Count == 2)
                return string.Format("Ya dispone de dos reservaciones confirmadas, no puede acceder a una nueva.");            

            if (propiedadReservas.Any(t => Convert.ToDateTime(reservaModelo.FechaReserva).CompareTo(Convert.ToDateTime(t.FechaReserva)) >= 0
                                     && Convert.ToDateTime(reservaModelo.FechaReserva).CompareTo(Convert.ToDateTime(t.FechaReserva).AddDays(7)) <= 0))            
                return string.Format("La semana elegida no esta disponible para la propiedad seleccionada.");

            if (clienteCreditos.Any(t => Convert.ToDateTime(reservaModelo.FechaReserva).CompareTo(Convert.ToDateTime(t.FechaReserva)) >= 0
                                && Convert.ToDateTime(reservaModelo.FechaReserva).CompareTo(Convert.ToDateTime(t.FechaReserva).AddDays(7)) <= 0))            
                return string.Format("Ya posee una reserva en la misma semana en otra propiedad.");

            if (propiedadSubastas.Any())
                foreach (var subastaBorrar in propiedadSubastas)
                    servicioSubasta.RemoverSubasta(subastaBorrar.IdSubasta);

                RESERVA nuevaReserva = new RESERVA();

                nuevaReserva.IdCliente = reservaModelo.IdCliente;
                nuevaReserva.Fecha = Convert.ToDateTime(reservaModelo.FechaReserva);
                nuevaReserva.IdPropiedad = reservaModelo.IdPropiedad;

                this.HomeSwitchDB.RESERVA.Add(nuevaReserva);
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Reservas");

                return "OK";
         }

        public bool CancelarReservaCliente(int idReserva)
        {
            var reservaBorrar = this.HomeSwitchDB.RESERVA.SingleOrDefault(t => t.IdReserva == idReserva);

            if (reservaBorrar != null && Convert.ToDateTime(reservaBorrar.Fecha.Date) >= DateTime.Now.Date.AddMonths(6))
            {
                this.HomeSwitchDB.RESERVA.Remove(reservaBorrar);
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Reservas");
                return true;
            }

            return false;
        }

        public bool CancelarReservaAdministrador(int idReserva)
        {
            var reservaBorrar = this.HomeSwitchDB.RESERVA.SingleOrDefault(t => t.IdReserva == idReserva);

            if (reservaBorrar != null)
            {
                this.HomeSwitchDB.RESERVA.Remove(reservaBorrar);
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Reservas");
                return true;
            }

            return false;
        }

        public List<ReservaViewModel> ObtenerReservas()
        {
            List<ReservaViewModel> reservasActuales;

            if (!CacheHomeSwitchHome.ExistOnCache("Reservas"))
            {
                reservasActuales = new List<ReservaViewModel>();
                var reservasBD = HomeSwitchDB.RESERVA.ToList();

                foreach (var reserva in reservasBD)
                {
                    reservasActuales.Add(new ReservaViewModel().ToViewModel(reserva));
                }

                CacheHomeSwitchHome.SaveToCache("Reservas", reservasActuales);
            }

            reservasActuales = (List<ReservaViewModel>)CacheHomeSwitchHome.GetFromCache("Reservas");

            return reservasActuales;
        }

        public List<ReservaViewModel> ObtenerReservasCliente(int idCliente)
        {
            var reservasCliente = this.ObtenerReservas();
            return reservasCliente.Where(t => t.IdCliente == idCliente &&  DateTime.Now <= Convert.ToDateTime(t.FechaReserva)).ToList();
        }

        public List<ReservaViewModel> ObtenerReservasPropiedad(int idPropiedad)
        {
            var reservasPropiedad = this.ObtenerReservas();
            return reservasPropiedad.Where(t => t.IdPropiedad == idPropiedad && DateTime.Now <= Convert.ToDateTime(t.FechaReserva)).ToList();
        }
    }
}