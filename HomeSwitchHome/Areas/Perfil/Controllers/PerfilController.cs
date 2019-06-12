using HomeSwitchHome.Services;
using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Perfil.Controllers
{
    public class PerfilController : Controller
    {
        readonly IUsuarioService servicioUsuario;
        readonly IReservaService servicioReserva;

        public PerfilController(IUsuarioService serviceUsuario, IReservaService serviceReserva)
        {
            this.servicioUsuario = serviceUsuario;
            this.servicioReserva = serviceReserva;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerMiInformacionPersonal()
        {
            var sesionUser = (ClienteViewModel)Session["ClienteActual"];
            return Json(sesionUser, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult ObtenerMisReservas()
        {
            var sesionUser = (ClienteViewModel)Session["ClienteActual"];

            return Json(this.servicioReserva.ObtenerReservasCliente(sesionUser.IdCliente).ToArray(), JsonRequestBehavior.AllowGet);
        }

    }
}
