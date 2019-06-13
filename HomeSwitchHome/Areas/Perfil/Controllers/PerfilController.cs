using HomeSwitchHome.Services;
using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var nacimiento = Convert.ToDateTime(sesionUser.FechaDeNacimiento);
            sesionUser.FechaDeNacimiento = string.Format("{0}-{1}-{2}", nacimiento.Day, nacimiento.Month, nacimiento.Year);

            return Json(sesionUser, JsonRequestBehavior.AllowGet); 
        }

        public JsonResult ObtenerMisReservas()
        {
            var sesionUser = (ClienteViewModel)Session["ClienteActual"];

            return Json(this.servicioReserva.ObtenerReservasCliente(sesionUser.IdCliente).ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SolicitarSubscripcionPremium()
        {
            var sesionUser = (ClienteViewModel)Session["ClienteActual"];
            var seRegistro= this.servicioUsuario.RegistrarComoPremium(sesionUser.IdCliente);

            if (!seRegistro) {

                this.servicioUsuario.RegistrarComoPremium(sesionUser.IdCliente);

                return Json("Ok", JsonRequestBehavior.AllowGet);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(string.Format("Ya se esta procesando una solicitud premium. Sera notificado cuando sea aprobado."), JsonRequestBehavior.AllowGet);
        }
    }
}
