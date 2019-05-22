using System.Web.Mvc;
using HomeSwitchHome.Services;

namespace HomeSwitchHome.Controllers
{
    public class HomeController : Controller
    {

        private PropiedadController propiedadController = new PropiedadController();
        private SubastaController subastaController = new SubastaController();

        public ActionResult Index()
        {
            var arraySubastas = subastaController.GetSubastas();
            return View(arraySubastas);
        }

        public ActionResult Propiedades()
        {
            return View();
        }

        public ActionResult Subastas()
        {
            return View();
        }

        public JsonResult GetPropiedades()
        {
            var arrayPropiedades = propiedadController.GetPropiedades();

            return Json(arrayPropiedades.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubastas()
        {
            var arraySubastas = subastaController.GetSubastas();

            return Json(arraySubastas.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}