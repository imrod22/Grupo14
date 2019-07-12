using HomeSwitchHome.Services;
using HomeSwitchHome.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Perfil.Controllers
{
    public class PerfilController : Controller
    {
        readonly IUsuarioService servicioUsuario;
        readonly IReservaService servicioReserva;
        readonly ICreditoService servicioCredito;

        public PerfilController(IUsuarioService serviceUsuario, IReservaService serviceReserva, ICreditoService creditoService)
        {
            this.servicioUsuario = serviceUsuario;
            this.servicioReserva = serviceReserva;
            this.servicioCredito = creditoService;
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
            var solicitudPremium = this.servicioUsuario.ObtenerSolicitudPremium(sesionUser.IdCliente);

            if (solicitudPremium == null) {

                this.servicioUsuario.RegistrarComoPremium(sesionUser.IdCliente);
                return Json("Su solicitud se ha registrado y esta siendo procesada se le notificara cuando pueda comenzar a operar como PREMIUM.", JsonRequestBehavior.AllowGet);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(string.Format("Ya se esta procesando una solicitud desde su cuenta. Se le notificará a la brevedad por email."), JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelarReserva(int idReserva)
        {
            if (this.servicioReserva.CancelarReserva(idReserva))
            {
                var reserva = this.servicioReserva.ObtenerReservas().Where(t => t.IdReserva == idReserva).SingleOrDefault();

                if (DateTime.Now >= DateTime.Parse(reserva.FechaReserva).AddMonths(2))
                {
                    var anioReserva = DateTime.Parse(reserva.FechaReserva).Year;

                    this.servicioCredito.DevolverCreditoCliente(reserva.IdCliente, anioReserva);
                    return Json(string.Format("Se ha cancelado su reserva en la residencia: {0} y se le ha devuelto el credito para el año {1}.", reserva.Propiedad.Nombre, anioReserva), JsonRequestBehavior.AllowGet);
                }

                return Json(string.Format("Se ha cancelado satisfactoriamente su reserva en la residencia: {0}, para la reserva faltaban menos de 2 meses, no es posible recuperar el credito.", reserva.Propiedad.Nombre), JsonRequestBehavior.AllowGet);

            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Ha ocurrido un error en el servidor y no se ha podido cancelar la reserva.", JsonRequestBehavior.AllowGet);            
        }
    }
}