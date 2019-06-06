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
   
        public JsonResult ObtenerInformacionPropiedad(int idPropiedad)
        {
            var propiedadesActuales = this.propiedadService.ObtenerPropiedades();
            var currentPropiedad = propiedadesActuales.Where(t => t.IdPropiedad == idPropiedad).SingleOrDefault();
            return Json(currentPropiedad, JsonRequestBehavior.AllowGet);
        }


    }
}