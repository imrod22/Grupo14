using HomeSwitchHome.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            nuevaResidencia.Ubicacion = domicilio;

            return Json(this.propiedadService.CrearPropiedad(nuevaResidencia), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerInformacionPropiedad(int idPropiedad)
        {
            var propiedadesActuales = this.propiedadService.ObtenerPropiedades();

            var currentPropiedad = propiedadesActuales.Where(t => t.IdPropiedad == idPropiedad).SingleOrDefault();

            return Json(currentPropiedad, JsonRequestBehavior.AllowGet);
        }
    }
}