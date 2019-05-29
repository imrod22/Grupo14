using System.Collections.Generic;
using System.Linq;
using HomeSwitchHome.ViewModels;

namespace HomeSwitchHome.Services
{
    public class PropiedadService : IPropiedadService
    {
        private HomeSwitchHomeDB HomeSwitchDB;

        public PropiedadService()
        {
            this.HomeSwitchDB = new HomeSwitchHomeDB();
        }

        public List<PropiedadViewModel> ObtenerPropiedades()
        {
            List<PropiedadViewModel> propiedadesActuales;

            if (!CacheHomeSwitchHome.ExistOnCache("Propiedades"))
            {
                propiedadesActuales = new List<PropiedadViewModel>();
                var propiedadesBD = HomeSwitchDB.PROPIEDAD.ToList();

                foreach (var propiedad in propiedadesBD)
                {
                    propiedadesActuales.Add(new PropiedadViewModel().ToViewModel(propiedad));
                }

                CacheHomeSwitchHome.SaveToCache("Propiedades", propiedadesActuales);
            }

            propiedadesActuales = (List<PropiedadViewModel>)CacheHomeSwitchHome.GetFromCache("Propiedades");
            
            return propiedadesActuales;
        }       

        public bool CrearPropiedad(PROPIEDAD nuevaPropiedad)
        {
            List<PropiedadViewModel> propiedadesActuales = this.ObtenerPropiedades();

            if (!propiedadesActuales.Any(t => t.Nombre == nuevaPropiedad.Nombre) || nuevaPropiedad.Nombre != null)
            {
                this.HomeSwitchDB.PROPIEDAD.Add(nuevaPropiedad);
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Propiedades");
                
                this.ObtenerPropiedades();

                return true;
            }

            else return false;
        }

        public bool ActualizarPropiedad(PROPIEDAD datosPrioridad, int idPropiedad)
        {
            var propiedadesActuales = this.ObtenerPropiedades();

            if (!propiedadesActuales.Any(t => t.Nombre == datosPrioridad.Nombre))
            {
                var propiedadModelo = this.HomeSwitchDB.PROPIEDAD.SingleOrDefault(t => t.IdPropiedad == idPropiedad);

                propiedadModelo.Nombre = datosPrioridad.Nombre;
                propiedadModelo.Descripcion = datosPrioridad.Descripcion;
                propiedadModelo.Ubicacion = datosPrioridad.Ubicacion;
                propiedadModelo.Pais = datosPrioridad.Pais;

                this.HomeSwitchDB.SaveChanges();

                CacheHomeSwitchHome.RemoveOnCache("Propiedades");
                this.ObtenerPropiedades();

                return true;
            }

            else return false;
        }
    }
}