using System.Web.Mvc;
using HomeSwitchHome.Services;

namespace HomeSwitchHome.Controllers
{
    public class HomeController : Controller
    {

        private PropiedadController propiedadController = new PropiedadController();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
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
            var arraySubastas = propiedadController.GetPropiedades();

            return Json(arraySubastas.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}