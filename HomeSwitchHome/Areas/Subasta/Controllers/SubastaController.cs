using HomeSwitchHome.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Subasta.Controllers
{
    public class SubastaController : Controller
    {
        readonly ISubastaService servicioSubasta;

        public SubastaController(ISubastaService subastaServicio)
        {
            this.servicioSubasta = subastaServicio;
        }

        // GET: Subasta/Subasta
        public ActionResult Index()
        {   
            return View();
        }

        public JsonResult Subastas()
        {
            return Json(this.servicioSubasta.ObtenerSubastasActivas().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PujarEnSubasta(string idSubasta, string valorPujado)
        {
            SUBASTA subastaPujada = new SUBASTA();
            subastaPujada.ValorActual = Convert.ToDecimal(valorPujado);

            if (this.servicioSubasta.PujarSubasta(subastaPujada, Int32.Parse(idSubasta)))
                return Json(this.servicioSubasta.ObtenerSubastasFuturas().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult ObtenerInformacionSubasta(int idSubasta)
        {
            var subastasActuales = this.servicioSubasta.ObtenerSubastasActivas();

            var currentPropiedad = subastasActuales.Where(t => t.IdSubasta == idSubasta).SingleOrDefault();

            return Json(currentPropiedad, JsonRequestBehavior.AllowGet);
        }
    }
}