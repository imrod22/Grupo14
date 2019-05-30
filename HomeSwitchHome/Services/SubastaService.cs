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

        public List<SubastaViewModel> ObtenerSubastas()
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
                    subastaActualizar.IdCliente = 1; //HARCODEADO SIEMPRE PUJA EL 1er USUARIO.

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

        public bool RemoverSubasta(int idSubasta)
        {
            var subastaABorrar = this.HomeSwitchDB.SUBASTA.SingleOrDefault(t => t.IdPropiedad == idSubasta);
            
            if (subastaABorrar != null && subastaABorrar.FechaComienzo.AddDays(3).CompareTo(DateTime.Now) == 0)
            {
                this.HomeSwitchDB.SUBASTA.Remove(subastaABorrar);
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Subastas");

                return true;
            }
            else
                return false;
        }
    }
}