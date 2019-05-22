using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyCache;

namespace HomeSwitchHome.Services
{
    public class PropiedadController
    {
        private HomeSwitchHomeDB HomeSwitchDB;
        IAppCache HomeSwitchCache = new CachingService();

        public PropiedadController()
        {
            this.HomeSwitchDB = new HomeSwitchHomeDB();
        }

        public List<PropiedadViewModel> GetPropiedades()
        {
            List<PropiedadViewModel> propiedadesToShow = (List<PropiedadViewModel>)HomeSwitchCache.CacheProvider.Get("propiedades");

            if (propiedadesToShow == null)
            {
                propiedadesToShow = new List<PropiedadViewModel>();
                var propiedadesBD = this.HomeSwitchDB.PROPIEDAD.ToList();

                foreach (var propiedad in propiedadesBD) {

                    propiedadesToShow.Add(new PropiedadViewModel().ToViewModel(propiedad));             
                }

                HomeSwitchCache.CacheProvider.Set("propiedades", propiedadesToShow, null);
            }    

            return propiedadesToShow;
        }       

        public void SavePropiedad(PROPIEDAD nuevaPropiedad)
        {
            this.HomeSwitchDB.PROPIEDAD.Add(nuevaPropiedad);
            this.HomeSwitchDB.SaveChanges();
        }

    }
}