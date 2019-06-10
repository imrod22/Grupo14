using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace HomeSwitchHome.Services
{
    public class SubastaService : ISubastaService
    {
        private HomeSwitchHomeDB HomeSwitchDB;

        public SubastaService()
        {
            this.HomeSwitchDB = new HomeSwitchHomeDB();
        }       

        public bool CrearSubasta(SUBASTA nuevaSubasta)
        {
            List<SubastaViewModel> subastas = this.ObtenerSubastas();

            if (!subastas.Any(t => Convert.ToDateTime(t.FechaComienzo) >= nuevaSubasta.FechaComienzo && Convert.ToDateTime(t.FechaComienzo).AddDays(7) < nuevaSubasta.FechaComienzo))
            {
                this.HomeSwitchDB.SUBASTA.Add(nuevaSubasta);
                this.HomeSwitchDB.SaveChanges();

                CacheHomeSwitchHome.RemoveOnCache("Subastas");

                return true;
            }
            else
                return false;
        }
        
        public bool PujarSubasta(SUBASTA subastaPujada, int idSubasta, int idCliente)
        {
                var subastaActualizar = this.HomeSwitchDB.SUBASTA.SingleOrDefault(t => t.IdSubasta == idSubasta);

                if (subastaActualizar != null 
                        && DateTime.Now < subastaActualizar.FechaComienzo.AddDays(3) 
                        && subastaActualizar.FechaComienzo <= DateTime.Now 
                        && subastaActualizar.ValorActual < subastaPujada.ValorActual && subastaActualizar.ValorMinimo < subastaPujada.ValorActual)
                {
                    subastaActualizar.ValorActual = subastaPujada.ValorActual;
                    subastaActualizar.IdCliente = idCliente;

                    this.HomeSwitchDB.SaveChanges();
                    CacheHomeSwitchHome.RemoveOnCache("Subastas");

                    return true;
                }
                else
                    return false;
        }

        public bool ActualizarSubasta(SUBASTA subastaActualizada, int idSubasta)
        {
            if (subastaActualizada.FechaComienzo.CompareTo(DateTime.Now) > 0 && subastaActualizada.ValorMinimo > 0)
            {
                var subastaModelo = this.HomeSwitchDB.SUBASTA.SingleOrDefault(t => t.IdSubasta == idSubasta);
                subastaModelo.FechaComienzo = subastaActualizada.FechaComienzo;
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