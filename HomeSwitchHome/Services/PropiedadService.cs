using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyCache;
using System.Web.Mvc;

namespace HomeSwitchHome.Services
{
    public class PropiedadService :IPropiedadService
    {
        private HomeSwitchHomeDB HomeSwitchDB;
        IAppCache HomeSwitchCache = new CachingService();

        public PropiedadService()
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

        public string SavePropiedad(PROPIEDAD nuevaPropiedad)
        {
            List<PropiedadViewModel> propiedades = (List<PropiedadViewModel>)HomeSwitchCache.CacheProvider.Get("propiedades");

            if (propiedades.Where(t => t.Nombre == nuevaPropiedad.Nombre).Any())
            {
                return null;
            }

            try
            {
                this.HomeSwitchDB.PROPIEDAD.Add(nuevaPropiedad);
                this.HomeSwitchDB.SaveChanges();

                var propiedadesToShow = new List<PropiedadViewModel>();
                var propiedadesBD = this.HomeSwitchDB.PROPIEDAD.ToList();

                foreach (var propiedad in propiedadesBD)
                {

                    propiedadesToShow.Add(new PropiedadViewModel().ToViewModel(propiedad));
                }

                HomeSwitchCache.CacheProvider.Set("propiedades", propiedadesToShow, null);

                return "Ok";
            }
            catch (Exception)
            {

                return null;
            }            
        }

        public string UpdatePropiedad(PROPIEDAD propiedad)
        {
            using (this.HomeSwitchDB)
            {
                var propiedadesBD = this.HomeSwitchDB.PROPIEDAD;
                if (propiedadesBD.Where(t => t.Nombre == propiedad.Nombre).Any())
                {
                    return null;
                }
                else {
                    var currentProp = this.HomeSwitchDB.PROPIEDAD.SingleOrDefault(t => t.IdPropiedad == propiedad.IdPropiedad);

                    currentProp.Nombre = propiedad.Nombre;
                    currentProp.Descripcion = propiedad.Descripcion;
                    currentProp.Domicilio = propiedad.Domicilio;
                    currentProp.Pais = propiedad.Pais;
                    this.HomeSwitchDB.SaveChanges();

                    var propiedadesToShow = new List<PropiedadViewModel>();
                    propiedadesBD = this.HomeSwitchDB.PROPIEDAD;

                    foreach (var propiedadNew in propiedadesBD.ToList())
                    {

                        propiedadesToShow.Add(new PropiedadViewModel().ToViewModel(propiedadNew));
                    }

                    HomeSwitchCache.CacheProvider.Set("propiedades", propiedadesToShow, null);

                    return "ok";
                }
            }


        }
    }
}