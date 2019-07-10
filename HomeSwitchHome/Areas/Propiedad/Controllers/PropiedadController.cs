using HomeSwitchHome.Services;
using HomeSwitchHome.ViewModels;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Propiedad.Controllers
{
    public class PropiedadController : Controller
    {
        readonly IPropiedadService propiedadService;
        readonly IReservaService reservaService;

        public PropiedadController(IPropiedadService propiedadServicio, IReservaService reservaServicio)
        {
            this.propiedadService = propiedadServicio;
            this.reservaService = reservaServicio;
        }

        public ActionResult Index()
        {            
            return View();
        }

        public JsonResult Propiedades()
        {
            return Json(this.propiedadService.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);
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

            var mensaje = this.reservaService.AgregarReserva(reservaNueva);

            if (mensaje != "OK")
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

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