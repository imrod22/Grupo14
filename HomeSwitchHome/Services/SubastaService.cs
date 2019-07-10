using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeSwitchHome.Services
{
    public class SubastaService : ISubastaService
    {
        private HomeSwitchHomeDB HomeSwitchDB;

        public SubastaService()
        {
            this.HomeSwitchDB = new HomeSwitchHomeDB();
        }

        public string CrearSubasta(SUBASTA nuevaSubasta)
        {
            List<SubastaViewModel> subastasPropiedad = this.ObtenerSubastasDePropiedad(nuevaSubasta.IdPropiedad);
            ReservaService servicioReserva = new ReservaService();

            var reservasPropiedad = servicioReserva.ObtenerReservasPropiedad(nuevaSubasta.IdPropiedad);

            if (nuevaSubasta.ValorMinimo <= 0)
                return string.Format("No se pudo crear la subasta, el valor de inicial para pujar debe ser mayor que 0.");
            
            if (nuevaSubasta.FechaReserva.CompareTo(nuevaSubasta.FechaComienzo.AddDays(3)) < 0)
                return string.Format("No se pudo crear la subasta, la semana que se desea subastar transcurre durante la subasta.");

            if (subastasPropiedad.Any(t => nuevaSubasta.FechaComienzo.CompareTo(Convert.ToDateTime(t.FechaComienzo)) >= 0
                                     && nuevaSubasta.FechaComienzo.CompareTo(Convert.ToDateTime(t.FechaComienzo).AddDays(10)) <= 0))
                return string.Format("No se pudo crear la subasta para la propiedad seleccionada, ya posee una subasta definida durante los dias elegidos.");

            if (reservasPropiedad.Any(t => nuevaSubasta.FechaReserva.CompareTo(Convert.ToDateTime(t.FechaReserva)) >= 0
                                      && nuevaSubasta.FechaReserva.CompareTo(Convert.ToDateTime(t.FechaReserva).AddDays(7)) <= 0))
                return string.Format("No se pudo crear la subasta para la propiedad seleccionada, posee reservas confirmadas durante los dias elegidos a subastar.");

            this.HomeSwitchDB.SUBASTA.Add(nuevaSubasta);
            this.HomeSwitchDB.SaveChanges();

            CacheHomeSwitchHome.RemoveOnCache("Subastas");
            return "OK";
        }
        
        public string PujarSubasta(SUBASTA subastaPujada, int idSubasta, int idCliente)
        {
            var subastaActualizar = this.HomeSwitchDB.SUBASTA.SingleOrDefault(t => t.IdSubasta == idSubasta);
            
            var reservas = new ReservaService();

            if (subastaActualizar == null)
                return string.Format("Se ha producido un error en el servidor.");

            if(subastaActualizar.ValorActual >= subastaPujada.ValorActual || subastaActualizar.ValorMinimo >= subastaPujada.ValorActual)
                return string.Format("El valor ingresado es menor al valor actual.");

            if(reservas.ObtenerReservasCliente(idCliente).Count == 2)
                return string.Format("Ya posee dos reservas efectuadas, no puede participar de la puja.");

            if(reservas.ObtenerReservasCliente(idCliente).Any(t => (Convert.ToDateTime(t.FechaReserva).CompareTo(Convert.ToDateTime(subastaActualizar.FechaReserva)) <= 0
                && Convert.ToDateTime(t.FechaReserva).AddDays(7).CompareTo(Convert.ToDateTime(subastaActualizar.FechaReserva)) >= 0 )
            
                || (Convert.ToDateTime(subastaActualizar.FechaReserva).CompareTo(Convert.ToDateTime(t.FechaReserva)) <= 0
                && Convert.ToDateTime(subastaActualizar.FechaReserva).AddDays(7).CompareTo(Convert.ToDateTime(t.FechaReserva)) >= 0)

                ))

                return string.Format("Ya posee una reserva durante la semana que se esta subastando.");

            subastaActualizar.ValorActual = subastaPujada.ValorActual;
            subastaActualizar.IdCliente = idCliente;

            this.HomeSwitchDB.SaveChanges();
            CacheHomeSwitchHome.RemoveOnCache("Subastas");

            return string.Format("OK");
        }

        public bool ActualizarSubasta(SUBASTA subastaActualizada, int idSubasta)
        {
            if (subastaActualizada.ValorMinimo > 0)
            {
                var subastaModelo = this.HomeSwitchDB.SUBASTA.SingleOrDefault(t => t.IdSubasta == idSubasta);
                subastaModelo.ValorMinimo = subastaActualizada.ValorMinimo;
                
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Subastas");

                return true;
            }
            else
                return false;
        }

        public bool ConfirmarSubasta(int idSubasta)
        {
            var subastaModelo = this.HomeSwitchDB.SUBASTA.SingleOrDefault(t => t.IdSubasta == idSubasta);

            subastaModelo.Estado = string.Format("CONFIRMADO");
            this.HomeSwitchDB.SaveChanges();
            CacheHomeSwitchHome.RemoveOnCache("Subastas");

            return true;
        }

        public bool RemoverSubasta(int idSubasta)
        {
            var subastaABorrar = this.HomeSwitchDB.SUBASTA.SingleOrDefault(t => t.IdSubasta == idSubasta);
            
            if (subastaABorrar != null)
            {
                this.HomeSwitchDB.SUBASTA.Remove(subastaABorrar);
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Subastas");

                return true;
            }
            else
                return false;
        }

        public List<SubastaViewModel> ObtenerSubastasDePropiedad(int idPropiedad)
        {
            var subastas = this.ObtenerSubastas();

            return subastas.Where(t => t.IdPropiedad == idPropiedad).ToList();
        }

        public List<SubastaViewModel> ObtenerSubastasActivas()
        {
            var subastas = this.ObtenerSubastas();

            return subastas.Where(t => Convert.ToDateTime(t.FechaComienzo) <= DateTime.Now && DateTime.Now <= Convert.ToDateTime(t.FechaComienzo).AddDays(3) && t.Estado == "NUEVO").ToList();
        }

        public List<SubastaViewModel> ObtenerSubastasFinalizadas()
        {
            var subastas = this.ObtenerSubastas();

            return subastas.Where(t => Convert.ToDateTime(t.FechaComienzo).AddDays(3) < DateTime.Now && t.Estado == "NUEVO").ToList();
        }

        public List<SubastaViewModel> ObtenerSubastasFuturas()
        {
            var subastas = this.ObtenerSubastas();

            return subastas.Where(t => DateTime.Now < Convert.ToDateTime(t.FechaComienzo) && t.Estado == "NUEVO").ToList();
        }

        public List<SubastaViewModel> ObtenerSubastasDesdeHoy()
        {
            var subastas = this.ObtenerSubastas();

            return subastas.Where(t => DateTime.Now <= Convert.ToDateTime(t.FechaComienzo)).ToList();

        }

        public SubastaViewModel ObtenerSubasta(int idSubasta)
        {
            var subastas = this.ObtenerSubastas();
            return subastas.Where(t => t.IdSubasta == idSubasta).SingleOrDefault();
        }

        private List<SubastaViewModel> ObtenerSubastas()
        {
            List<SubastaViewModel> subastasActuales;

            if (!CacheHomeSwitchHome.ExistOnCache("Subastas"))
            {
                subastasActuales = new List<SubastaViewModel>();
                var subastasBD = HomeSwitchDB.SUBASTA.ToList();
                

                foreach (var subasta in subastasBD)
                {
                    var vistaSubasta = new SubastaViewModel().ToViewModel(subasta);

                    if(vistaSubasta.Propiedad == null)
                    {
                        var propiedadModelo = HomeSwitchDB.PROPIEDAD.SingleOrDefault(t => t.IdPropiedad == vistaSubasta.IdPropiedad);
                        vistaSubasta.Propiedad = new PropiedadViewModel().ToViewModel(propiedadModelo);
                    }

                    if(vistaSubasta.IdCliente != null && vistaSubasta.Cliente == null)
                    {
                        var clienteModelo = HomeSwitchDB.CLIENTE.SingleOrDefault(t => t.IdCliente == vistaSubasta.IdCliente);
                        vistaSubasta.Cliente = new ClienteViewModel().ToViewModel(clienteModelo);
                    }

                    subastasActuales.Add(vistaSubasta);

                }

                CacheHomeSwitchHome.SaveToCache("Subastas", subastasActuales);
            }

            subastasActuales = (List<SubastaViewModel>)CacheHomeSwitchHome.GetFromCache("Subastas");

            return subastasActuales;
        }
    }
}