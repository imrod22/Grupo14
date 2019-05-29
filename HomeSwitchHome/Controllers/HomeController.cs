using System.Web.Mvc;
using HomeSwitchHome.Services;

namespace HomeSwitchHome.Controllers
{
    public class HomeController : Controller
    {

        readonly ISubastaService subastaService;

        public HomeController(ISubastaService subastaServicio)
        {
            this.subastaService = subastaServicio;
        }

        public ActionResult Index()
        {
            return View(this.subastaService.ObtenerSubastas());
        }

        //public JsonResult NuevaPuja(string idSubasta, string valorActual)
        //{
        //    SUBASTA nuevaPuja = new SUBASTA();
        //    nuevaPuja.IdPropiedad = Convert.ToInt32(idSubasta);
        //    nuevaPuja.ValorActual = Convert.ToDecimal(valorActual);

        //    return Json(subastaController.PujarSubasta(nuevaPuja));
        //}
    }
}