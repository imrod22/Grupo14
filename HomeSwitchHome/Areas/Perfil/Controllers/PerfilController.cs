using HomeSwitchHome.Services;
using HomeSwitchHome.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Perfil.Controllers
{
    public class PerfilController : Controller
    {
        readonly IUsuarioService servicioUsuario;
        readonly IReservaService servicioReserva;
        readonly ICreditoService servicioCredito;
        readonly IHotsaleService servicioHotSale;

        public PerfilController(IUsuarioService serviceUsuario, IReservaService serviceReserva, ICreditoService creditoService, IHotsaleService serviceHotSale)
        {
            this.servicioUsuario = serviceUsuario;
            this.servicioReserva = serviceReserva;
            this.servicioCredito = creditoService;
            this.servicioHotSale = serviceHotSale;
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

        public JsonResult ObtenerReservasActuales()
        {
            var sesionUser = (ClienteViewModel)Session["ClienteActual"];
            var reservasAnio = this.servicioReserva.ObtenerReservasClientePorAnio(sesionUser.IdCliente, DateTime.Now.Year);
            return Json(reservasAnio.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerReservasProximas()
        {
            var sesionUser = (ClienteViewModel)Session["ClienteActual"];
            var reservasAnio = this.servicioReserva.ObtenerReservasClientePorAnio(sesionUser.IdCliente, DateTime.Now.Year + 1);
            return Json(reservasAnio.ToArray(), JsonRequestBehavior.AllowGet);
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
            var reserva = this.servicioReserva.ObtenerReservas().Where(t => t.IdReserva == idReserva).SingleOrDefault();
            var fechaReserva = DateTime.Parse(reserva.FechaReserva);

            string mensaje;

            if (fechaReserva <= DateTime.Now)
            {
                mensaje = "No se puede cancelar la reserva, ya ha sido efectuada.";
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(mensaje, JsonRequestBehavior.AllowGet);
            }

            if (this.servicioReserva.CancelarReserva(idReserva))
                {
                    if (reserva.Credito)
                    {
                        if (DateTime.Now.AddMonths(2) < fechaReserva)
                        {
                            var anioReserva = fechaReserva.Year;

                            this.servicioCredito.DevolverCreditoCliente(reserva.IdCliente, anioReserva);
                            return Json(string.Format("Se ha cancelado su reserva en la residencia: {0} y se le ha devuelto el credito para el año {1}.", reserva.Propiedad.Nombre, anioReserva), JsonRequestBehavior.AllowGet);
                        }

                        return Json(string.Format("Se ha cancelado satisfactoriamente su reserva en la residencia: {0}, para la reserva faltaban menos de 2 meses, no es posible recuperar el credito.", reserva.Propiedad.Nombre), JsonRequestBehavior.AllowGet);
                    }

                    else {
                        this.servicioHotSale.LiberarHotSale(fechaReserva, reserva.IdPropiedad);
                        return Json(string.Format("Se ha cancelado satisfactoriamente su reserva de HOT SALE y se ha liberado la semana."), JsonRequestBehavior.AllowGet);
                    }
                }
            mensaje = "Ha ocurrido un error en el servidor y no se ha podido cancelar la reserva.";
            
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(mensaje, JsonRequestBehavior.AllowGet);            
        }

        public JsonResult ObtenerCreditosActuales()
        {
            var sesionUser = (ClienteViewModel)Session["ClienteActual"];

            var creditoAnio = this.servicioCredito.ObtenerCreditosAnio(DateTime.Now.Year, sesionUser.IdCliente);

            return Json(creditoAnio.Credito, JsonRequestBehavior.AllowGet);

        }

        public JsonResult ObtenerCreditosProximos()
        {
            var sesionUser = (ClienteViewModel)Session["ClienteActual"];

            var creditoAnio = this.servicioCredito.ObtenerCreditosAnio(DateTime.Now.Year + 1, sesionUser.IdCliente);

            return Json(creditoAnio.Credito, JsonRequestBehavior.AllowGet);

        }

        public JsonResult CambiarContraseña(string vieja, string nueva)
        {
            var sesionUser = (ClienteViewModel)Session["ClienteActual"];
            var infoUsuario = this.servicioUsuario.ObtenerInformacionDeUsuario(sesionUser.Usuario);

            if (!infoUsuario.Password.Equals(vieja))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(string.Format("La contraseña actual no es la registrada, verifique el campo ingresado."), JsonRequestBehavior.AllowGet);
            }                

            Regex rg = new Regex(@"^[a-zA-Z0-9\s,]*$");

            if (nueva.Count() < 8 || !rg.IsMatch(nueva))
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(string.Format("La nueva contraseña no tiene un formato alfanumerico de mas de 8 caracteres, verifique el campo ingresado."), JsonRequestBehavior.AllowGet);
            }                

            if(this.servicioUsuario.ActualizarContrasenia(sesionUser.IdCliente, nueva))
                return Json(string.Format("Se ha actualizado satisfactoriamente su contraseña, ya puede iniciar sesion con la nueva ingresada."), JsonRequestBehavior.AllowGet);

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(string.Format("Ha ocurrido un error en el servidor y no se ha podido actualizar la contraseña."), JsonRequestBehavior.AllowGet);
        }
    }
}