using HomeSwitchHome.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Administrador.Controllers
{
    public class AdministradorController : Controller
    {
        readonly IUsuarioService servicioUsuario;
        readonly ISubastaService servicioSubasta;
        readonly IPropiedadService servicioPropiedad;

        public AdministradorController(IPropiedadService propiedadService, ISubastaService subastaService, IUsuarioService usuarioService)
        {
            this.servicioPropiedad = propiedadService;
            this.servicioSubasta = subastaService;
            this.servicioUsuario = usuarioService;
        }

        // GET: Administrador/Administrador

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Propiedades()
        {
            return Json(this.servicioPropiedad.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubastasSinEmpezar()
        {
            return Json(this.servicioSubasta.ObtenerSubastasFuturas().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubastasCerradas()
        {
            return Json(this.servicioSubasta.ObtenerSubastasFinalizadas().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult NuevosClientes()
        {
            return Json(this.servicioUsuario.ObtenerNuevosClientes().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClientesAPremium()
        {
            return Json(this.servicioUsuario.ObtenerSolicitudesPremium().ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}