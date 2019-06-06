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
        
        public bool PujarSubasta(SUBASTA subastaPujada, int idSubasta)
        {
                var subastaActualizar = this.HomeSwitchDB.SUBASTA.SingleOrDefault(t => t.IdSubasta == idSubasta);

                if (subastaActualizar != null 
                        && DateTime.Now < subastaActualizar.FechaComienzo.AddDays(3) 
                        && subastaActualizar.FechaComienzo <= DateTime.Now 
                        && subastaActualizar.ValorActual < subastaPujada.ValorActual && subastaActualizar.ValorMinimo < subastaPujada.ValorActual)
                {
                    subastaActualizar.ValorActual = subastaPujada.ValorActual;
                    subastaActualizar.IdCliente = 1;

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
                var propiedadModelo = this.HomeSwitchDB.SUBASTA.SingleOrDefault(t => t.IdSubasta == idSubasta);
                propiedadModelo.FechaComienzo = subastaActualizada.FechaComienzo;
                propiedadModelo.ValorMinimo = subastaActualizada.ValorMinimo;
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
            
            if (subastaABorrar != null && subastaABorrar.FechaComienzo > DateTime.Now)
            {
                this.HomeSwitchDB.SUBASTA.Remove(subastaABorrar);
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Subastas");

                return true;
            }
            else
                return false;
        }

        public List<SubastaViewModel> ObtenerSubastasActivas()
        {
            var subastas = this.ObtenerSubastas();

            return subastas.Where(t => Convert.ToDateTime(t.FechaComienzo) <= DateTime.Now && DateTime.Now <= Convert.ToDateTime(t.FechaComienzo).AddDays(3)).ToList();
        }

        public List<SubastaViewModel> ObtenerSubastasFinalizadas()
        {
            var subastas = this.ObtenerSubastas();

            return subastas.Where(t => Convert.ToDateTime(t.FechaComienzo).AddDays(3) < DateTime.Now && t.Estado == "NUEVO").ToList();
        }

        public List<SubastaViewModel> ObtenerSubastasFuturas()
        {
            var subastas = this.ObtenerSubastas();

            return subastas.Where(t => DateTime.Now < Convert.ToDateTime(t.FechaComienzo)).ToList();
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
                    subastasActuales.Add(new SubastaViewModel().ToViewModel(subasta));
                }

                CacheHomeSwitchHome.SaveToCache("Subastas", subastasActuales);
            }

            subastasActuales = (List<SubastaViewModel>)CacheHomeSwitchHome.GetFromCache("Subastas");

            return subastasActuales;
        }
    }
}