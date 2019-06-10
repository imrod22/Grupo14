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

        public bool AgregarReserva(ReservaViewModel reservaModelo)
        {
            RESERVA nuevaReserva = new RESERVA();

            nuevaReserva.IdCliente = reservaModelo.IdCliente;
            nuevaReserva.Fecha = Convert.ToDateTime(reservaModelo.FechaReserva);
            nuevaReserva.IdPropiedad = reservaModelo.IdPropiedad;

            this.HomeSwitchDB.RESERVA.Add(nuevaReserva);
            this.HomeSwitchDB.SaveChanges();
            CacheHomeSwitchHome.RemoveOnCache("Reservas");

            return true;
                
         }

        public bool CancelarReserva(int idReserva)
        {
            var reservaBorrar = this.HomeSwitchDB.RESERVA.SingleOrDefault(t => t.IdReserva == idReserva);

            if (reservaBorrar != null && Convert.ToDateTime(reservaBorrar.Fecha) > DateTime.Now.AddMonths(6))
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
            return reservasCliente.Where(t => t.IdCliente == idCliente &&  DateTime.Now <= Convert.ToDateTime(t.FechaReserva) && Convert.ToDateTime(t.FechaReserva) <= DateTime.Now.AddYears(1)).ToList();
        }

        public List<ReservaViewModel> ObtenerReservasPropiedad(int idPropiedad)
        {
            var reservasPropiedad = this.ObtenerReservas();
            return reservasPropiedad.Where(t => t.IdPropiedad == idPropiedad && DateTime.Now <= Convert.ToDateTime(t.FechaReserva) && Convert.ToDateTime(t.FechaReserva) <= DateTime.Now.AddYears(1)).ToList();
        }
    }
}