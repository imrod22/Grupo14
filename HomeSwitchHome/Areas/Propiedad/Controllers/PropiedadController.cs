using HomeSwitchHome.Services;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Propiedad.Controllers
{
    public class PropiedadController : Controller
    {
        readonly IPropiedadService propiedadService;

        public PropiedadController(IPropiedadService propiedadServicio)
        {
            this.propiedadService = propiedadServicio;
        }

        public ActionResult Index()
        {            
            return View();
        }

        public JsonResult Propiedades()
        {
            return Json(this.propiedadService.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CrearPropiedad(string nombre, string domicilio, string descripcion, string pais)
        {
            PROPIEDAD nuevaResidencia = new PROPIEDAD();
            nuevaResidencia.Nombre = nombre;
            nuevaResidencia.Pais = pais;
            nuevaResidencia.Descripcion = descripcion;

            if(this.propiedadService.CrearPropiedad(nuevaResidencia))
               return Json(this.propiedadService.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult ObtenerInformacionPropiedad(int idPropiedad)
        {
            var propiedadesActuales = this.propiedadService.ObtenerPropiedades();

            var currentPropiedad = propiedadesActuales.Where(t => t.IdPropiedad == idPropiedad).SingleOrDefault();

            return Json(currentPropiedad, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarPropiedad(string idpropiedad, string descripcion, string pais)
        {
            PROPIEDAD nuevaResidencia = new PROPIEDAD();

            nuevaResidencia.Descripcion = descripcion;
            nuevaResidencia.Pais = pais;

            if (this.propiedadService.ActualizarPropiedad(nuevaResidencia, Int32.Parse(idpropiedad)))
                return Json(this.propiedadService.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult BorrarPropiedad(string idpropiedad)
        {
            if (this.propiedadService.RemoverPropiedad(Int32.Parse(idpropiedad)))
                return Json(this.propiedadService.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);

            return null;

        }
    }
}