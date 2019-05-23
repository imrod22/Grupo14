using HomeSwitchHome.ViewModels;
using LazyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeSwitchHome.Services
{
    public class SubastaController : Controller
    {
        private HomeSwitchHomeDB HomeSwitchDB;
        IAppCache HomeSwitchCache = new CachingService();

        public SubastaController()
        {
            this.HomeSwitchDB = new HomeSwitchHomeDB();
        }


        public List<SubastaViewModel> GetSubastas()
            {
                List<SubastaViewModel> subastasToShow = (List<SubastaViewModel>)HomeSwitchCache.CacheProvider.Get("subastas");

                if (subastasToShow == null)
                {
                    subastasToShow = new List<SubastaViewModel>();
                    var subastaDB = this.HomeSwitchDB.SUBASTA.ToList();

                    foreach (var subasta in subastaDB)
                    {
                        subastasToShow.Add(new SubastaViewModel().ToViewModel(subasta));
                    }

                    HomeSwitchCache.CacheProvider.Set("subastas", subastasToShow, null);
                }

                return subastasToShow;
            }


        public string SaveSubasta(SUBASTA nuevaSubasta)
        {
            List<SubastaViewModel> subastas = (List<SubastaViewModel>)HomeSwitchCache.CacheProvider.Get("subastas");

            var currentSubastas = subastas.Where(t => t.Propiedad.IdPropiedad == nuevaSubasta.IdPropiedad);

            if (currentSubastas.Where(t => t.FechaComienzo >= nuevaSubasta.FechaComienzo && t.FechaComienzo.AddDays(7) < nuevaSubasta.FechaComienzo).Any())
            {
                return null;
            }

            try
            {
                this.HomeSwitchDB.SUBASTA.Add(nuevaSubasta);
                this.HomeSwitchDB.SaveChanges();

                var subastaDB = this.HomeSwitchDB.SUBASTA.ToList();
                var subastasToShow = new List<SubastaViewModel>();

                foreach (var subasta in subastaDB)
                {
                    subastasToShow.Add(new SubastaViewModel().ToViewModel(subasta));
                }

                HomeSwitchCache.CacheProvider.Set("subastas", subastasToShow, null);


                return "Ok";
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string PujarSubasta(SUBASTA subastaPujada)
        {
            using (this.HomeSwitchDB)
            {
                var result = this.HomeSwitchDB.SUBASTA.SingleOrDefault(t => t.IdSubasta == subastaPujada.IdSubasta);
                var subastasToShow = new List<SubastaViewModel>();

                if (result != null && subastaPujada.FechaComienzo >= DateTime.Now && result.ValorActual < subastaPujada.ValorActual && subastaPujada.ValorMinimo < result.ValorActual)
                {
                    result.ValorActual = subastaPujada.ValorActual;
                    this.HomeSwitchDB.SaveChanges();

                    var subastaDB = this.HomeSwitchDB.SUBASTA;

                    foreach (var subasta in subastaDB.ToList())
                    {
                        subastasToShow.Add(new SubastaViewModel().ToViewModel(subasta));
                    }

                    HomeSwitchCache.CacheProvider.Set("subastas", subastasToShow, null);

                    return "ok";
                }
                else return null;
            }

            
        }
    }
}