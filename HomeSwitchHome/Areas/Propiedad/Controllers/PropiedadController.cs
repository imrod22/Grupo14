using HomeSwitchHome.Services;
using HomeSwitchHome.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Propiedad.Controllers
{
    public class PropiedadController : Controller
    {
        readonly IPropiedadService propiedadService;
        readonly IReservaService reservaService;
        readonly ICreditoService creditoService;

        public PropiedadController(IPropiedadService propiedadServicio, IReservaService reservaServicio, ICreditoService creditoServicio)
        {
            this.propiedadService = propiedadServicio;
            this.reservaService = reservaServicio;
            this.creditoService = creditoServicio;
        }

        public ActionResult Index()
        {            
            return View();
        }

        public JsonResult Propiedades()
        {
            var propiedades = this.propiedadService.ObtenerPropiedades().ToArray();

            return Json(propiedades, JsonRequestBehavior.AllowGet);
        }
   
        public JsonResult ObtenerInformacionPropiedad(int idPropiedad)
        {
            var propiedadesActuales = this.propiedadService.ObtenerPropiedades();
            var currentPropiedad = propiedadesActuales.Where(t => t.IdPropiedad == idPropiedad).SingleOrDefault();
            return Json(currentPropiedad, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReservarPropiedad(int idPropiedad, string fecha)
        {
            var sesionUser = (ClienteViewModel)Session["ClienteActual"];
            ReservaViewModel reservaNueva = new ReservaViewModel();
            reservaNueva.IdPropiedad = idPropiedad;
            reservaNueva.IdCliente = sesionUser.IdCliente;
            reservaNueva.FechaReserva = fecha;
            reservaNueva.Credito = true;

            var mensaje = this.reservaService.AgregarReserva(reservaNueva);

            if (mensaje != "OK")
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            else {

                this.creditoService.DescontarCreditoCliente(sesionUser.IdCliente, DateTime.Parse(reservaNueva.FechaReserva).Year);

                if (this.reservaService.CancelarSubastasDePropiedadReservada(reservaNueva))
                    mensaje = string.Format("Se ha confirmado la reserva, se ha descontado un credito para el año {0} y se han cancelado las subastas definidas en el rango de fechas de la reserva.", DateTime.Parse(fecha).Year);
                else
                    mensaje = string.Format("Se ha confirmado la reserva y se ha descontado un credito para el año {0}.", DateTime.Parse(fecha).Year);
            }

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SolicitarNovedadPropiedad(int idPropiedad)
        {
            var sesionUser = (ClienteViewModel)Session["ClienteActual"];
            NovedadViewModel novedadNueva = new NovedadViewModel();
            novedadNueva.PropiedadId = idPropiedad;
            novedadNueva.ClienteId = sesionUser.IdCliente;

            if (this.propiedadService.RegistrarNotificacionesDePropiedad(novedadNueva))
            {
                return Json("Se registro la solicitud, será notificado a su cuenta de mail cuando haya novedades sobre la residencia elegida.", JsonRequestBehavior.AllowGet);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Ya esta siendo notificado sobre nuevas subastas o hot sales en la residencia elegida.", JsonRequestBehavior.AllowGet);
        }
    }
}