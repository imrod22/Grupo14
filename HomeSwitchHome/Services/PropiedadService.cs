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
            var propiedadesActuales = this.ObtenerTodasLasPropiedades().Where(t => t.Activa).ToList();
            
            return propiedadesActuales;
        }

        public List<PropiedadViewModel> ObtenerTodasLasPropiedades()
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
            List<PropiedadViewModel> propiedadesActuales = this.ObtenerTodasLasPropiedades();

            if (nuevaPropiedad.Pais != null && !propiedadesActuales.Any(t => t.Nombre.Trim().ToLower() == nuevaPropiedad.Nombre.Trim().ToLower()))
            {
                this.HomeSwitchDB.PROPIEDAD.Add(nuevaPropiedad);
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Propiedades");

                return true;
            }

            else return false;
        }

        public bool ActualizarPropiedad(PROPIEDAD datosPrioridad, int idPropiedad)
        {

            if (datosPrioridad.Descripcion != null && datosPrioridad.Pais != null && datosPrioridad.Descripcion.Length >= 20)
            {
                var propiedadModelo = this.HomeSwitchDB.PROPIEDAD.SingleOrDefault(t => t.IdPropiedad == idPropiedad);
                propiedadModelo.Descripcion = datosPrioridad.Descripcion;
                propiedadModelo.Pais = datosPrioridad.Pais;
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Propiedades");

                return true;
            }
            else 
            {
                return false;
            }
        }
        
        public bool BorrarPropiedad(int idPropiedad)
        {
            var propiedadABorrar = this.HomeSwitchDB.PROPIEDAD.SingleOrDefault(t => t.IdPropiedad == idPropiedad);
            var subastaService = new SubastaService();
            var propiedadAsViewModel = new PropiedadViewModel().ToViewModel(propiedadABorrar);
            var reservaService = new ReservaService();

            var subastasPropiedad = subastaService.ObtenerSubastasDePropiedad(idPropiedad);
            var reservaPropiedad = reservaService.ObtenerReservasPropiedad(idPropiedad);

            if (propiedadABorrar != null && !subastasPropiedad.Any() && !reservaPropiedad.Any())
            {
                this.HomeSwitchDB.PROPIEDAD.Remove(propiedadABorrar);
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Propiedades");

                return true;
            }
            else
                return false;            
        }

        public bool CambiarEstatusPropiedad(int idPropiedad)
        {
            var propiedadABorrar = this.HomeSwitchDB.PROPIEDAD.SingleOrDefault(t => t.IdPropiedad == idPropiedad);

            if (propiedadABorrar != null)
            {
                propiedadABorrar.Activa = !propiedadABorrar.Activa;
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Propiedades");
                return true;
            }

            return false;
        }

        public bool RegistrarNotificacionesDePropiedad(NovedadViewModel nuevaNovedad)
        {
            var novedadesParaCliente = this.ObtenerNovedadesDeCliente(nuevaNovedad.ClienteId);

            if (novedadesParaCliente.Where(t => t.IdPropiedad == nuevaNovedad.PropiedadId).Any())
                return false;

            NOVEDAD_PROPIEDAD novedadGuardar = new NOVEDAD_PROPIEDAD();
            novedadGuardar.IdPropiedad = nuevaNovedad.PropiedadId;
            novedadGuardar.IdCliente = nuevaNovedad.ClienteId;

            this.HomeSwitchDB.NOVEDAD_PROPIEDAD.Add(novedadGuardar);
            this.HomeSwitchDB.SaveChanges();

            return true;
        }

        public List<NOVEDAD_PROPIEDAD> ObtenerNovedadesDeCliente(int idCliente)
        {
            return this.HomeSwitchDB.NOVEDAD_PROPIEDAD.Where(t => t.IdCliente == idCliente).ToList();
        }

        public List<NOVEDAD_PROPIEDAD> ObtenerNotificaciones(int idPropiedad)
        {
            return this.HomeSwitchDB.NOVEDAD_PROPIEDAD.Where(t => t.IdPropiedad == idPropiedad).ToList();
        }

        public bool AgregarImagen(int idPropiedad, string nombreImagen)
        {
            var yaExiste = this.HomeSwitchDB.IMAGEN.Where(t => t.Nombre == nombreImagen).Any();

            if (yaExiste)
                return false;

            IMAGEN nuevaImagen = new IMAGEN();
            nuevaImagen.Nombre = nombreImagen;
            nuevaImagen.IdPropiedad = idPropiedad;
            nuevaImagen.Path = string.Format("/app-content/{0}", nombreImagen);

            this.HomeSwitchDB.IMAGEN.Add(nuevaImagen);
            this.HomeSwitchDB.SaveChanges();

            CacheHomeSwitchHome.RemoveOnCache("Propiedades");
            return true;

        }

        public bool EliminarImagen(int idImagen) {
            var imagenAEliminar = this.HomeSwitchDB.IMAGEN.Where(t => t.IdImagen == idImagen).SingleOrDefault();

            if (imagenAEliminar == null)
                return false;

            this.HomeSwitchDB.IMAGEN.Remove(imagenAEliminar);
            this.HomeSwitchDB.SaveChanges();

            CacheHomeSwitchHome.RemoveOnCache("Propiedades");
            return true;
        }
    }
}