using HomeSwitchHome.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Perfil.Controllers
{
    public class PerfilController : Controller
    {
        //readonly IUsuarioService servicioUsuario;
        readonly ISubastaService servicioSubasta;
        readonly IPropiedadService servicioPropiedad;

        public PerfilController(IPropiedadService propiedadService, ISubastaService subastaService)
        {
            this.servicioSubasta = subastaService;
            this.servicioPropiedad = propiedadService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult SubastasFinalizadas(int IdCliente)
        {
            var subFin = this.servicioSubasta.ObtenerSubastasFinalizadas().ToArray();
            var subastas = subFin.Where(prop => prop.Cliente.IdCliente == IdCliente);

            if (!subastas.Any())
            {
                return null;
            }

            return Json(subastas, JsonRequestBehavior.AllowGet);

        }
    }
}
