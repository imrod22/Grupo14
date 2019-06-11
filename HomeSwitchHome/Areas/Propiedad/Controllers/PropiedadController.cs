using HomeSwitchHome.Services;
using HomeSwitchHome.ViewModels;
using System;
using System.Linq;
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
            var reservasCliente = this.reservaService.ObtenerReservasCliente(sesionUser.IdCliente); 
            
            var fechaOcupada = this.reservaService.ObtenerReservasPropiedad(idPropiedad).Any(t => Convert.ToDateTime(t.FechaReserva) <= Convert.ToDateTime(fecha) && Convert.ToDateTime(fecha) <= Convert.ToDateTime(t.FechaReserva));

            if (reservasCliente.Count < 2 && !fechaOcupada)
            {
                ReservaViewModel reservaNueva = new ReservaViewModel();
                reservaNueva.IdPropiedad = idPropiedad;
                reservaNueva.IdCliente = sesionUser.IdCliente;
                reservaNueva.FechaReserva = fecha;                

                this.reservaService.AgregarReserva(reservaNueva);

                return Json(string.Format("Ok"), JsonRequestBehavior.AllowGet);
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }
    }
}