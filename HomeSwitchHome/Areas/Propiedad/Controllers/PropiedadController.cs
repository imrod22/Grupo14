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
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }
    }
}