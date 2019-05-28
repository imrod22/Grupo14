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
        
        public void CrearSubasta(SUBASTA nuevaSubasta)
        {
            List<SubastaViewModel> subastas = this.ObtenerSubastas();

            if (!subastas.Where(t => t.FechaComienzo >= nuevaSubasta.FechaComienzo && t.FechaComienzo.AddDays(7) < nuevaSubasta.FechaComienzo).Any())
            {              
                this.HomeSwitchDB.SUBASTA.Add(nuevaSubasta);
                this.HomeSwitchDB.SaveChanges();

                CacheHomeSwitchHome.RemoveOnCache("Subastas");
                this.ObtenerSubastas();
            }
        }
        
        public void PujarSubasta(SUBASTA subastaPujada)
        {
            using (this.HomeSwitchDB)
            {
                var subastaConNuevoValor = this.HomeSwitchDB.SUBASTA.SingleOrDefault(t => t.IdSubasta == subastaPujada.IdSubasta);

                if (subastaConNuevoValor != null && subastaPujada.FechaComienzo >= DateTime.Now && subastaConNuevoValor.ValorActual < subastaPujada.ValorActual && subastaPujada.ValorMinimo < subastaConNuevoValor.ValorActual)
                {
                    subastaConNuevoValor.ValorActual = subastaPujada.ValorActual;
                    this.HomeSwitchDB.SaveChanges();

                    CacheHomeSwitchHome.RemoveOnCache("Subastas");
                    this.ObtenerSubastas();
                }
            }            
        }
    }
}