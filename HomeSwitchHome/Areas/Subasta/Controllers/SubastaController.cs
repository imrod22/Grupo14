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
            return Json(this.servicioSubasta.ObtenerSubastas().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CrearSubasta(string idPropiedad, string fechaComienzo, string valorMinimo)
        {
            SUBASTA nuevaSubasta = new SUBASTA();
            nuevaSubasta.IdPropiedad = Int32.Parse(idPropiedad);
            nuevaSubasta.FechaComienzo = DateTime.Parse(fechaComienzo);
            nuevaSubasta.ValorMinimo = Convert.ToDecimal(valorMinimo);

            if (this.servicioSubasta.CrearSubasta(nuevaSubasta))
                return Json(this.servicioSubasta.ObtenerSubastas().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult ModificarSubasta(string idSubasta, string fechaComienzo, string valorMinimo)
        {
            SUBASTA subastaActualizada = new SUBASTA();
            subastaActualizada.FechaComienzo = DateTime.Parse(fechaComienzo);
            subastaActualizada.ValorMinimo = Convert.ToDecimal(valorMinimo);

            if (this.servicioSubasta.ActualizarSubasta(subastaActualizada, Int32.Parse(idSubasta)))
                return Json(this.servicioSubasta.ObtenerSubastas().ToArray(), JsonRequestBehavior.AllowGet);

            return Json(new { errorMessage = "Error" });
        }

        public JsonResult PujarEnSubasta(string idSubasta, string valorPujado)
        {
            SUBASTA subastaPujada = new SUBASTA();
            subastaPujada.ValorActual = Convert.ToDecimal(valorPujado);

            if (this.servicioSubasta.PujarSubasta(subastaPujada, Int32.Parse(idSubasta)))
                return Json(this.servicioSubasta.ObtenerSubastas().ToArray(), JsonRequestBehavior.AllowGet);

            return Json(new { errorMessage = "Error" });
        }

        public JsonResult BorrarPropiedad(string idSubasta)
        {
            if (this.servicioSubasta.RemoverSubasta(Int32.Parse(idSubasta)))
                return Json(this.servicioSubasta.ObtenerSubastas().ToArray(), JsonRequestBehavior.AllowGet);

            return Json(new { errorMessage = "Error" });
        }

        public JsonResult ObtenerInformacionSubasta(int idSubasta)
        {
            var subastasActuales = this.servicioSubasta.ObtenerSubastas();

            var currentPropiedad = subastasActuales.Where(t => t.IdSubasta == idSubasta).SingleOrDefault();

            return Json(currentPropiedad, JsonRequestBehavior.AllowGet);
        }

    }
}