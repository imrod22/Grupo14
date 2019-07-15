using HomeSwitchHome.Services;
using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Hotsale.Controllers
{
    public class HotsaleController : Controller
    {
        readonly IHotsaleService servicioHotSale;
        readonly IReservaService reservaService;

        public HotsaleController(IHotsaleService serviceHotSale, IReservaService serviceReserva)
        {
            this.servicioHotSale = serviceHotSale;
            this.reservaService = serviceReserva;
        }

        // GET: Hotsale/Hotsale
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ObtenerHotSales()
        {
            var hotsales = this.servicioHotSale.ObtenerHotSalesFuturos();
            return Json(hotsales.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ReservarHotSale(int idHotSale)
        {
            var sesionUser = (ClienteViewModel)Session["ClienteActual"];
            var hotSalesActuales = this.servicioHotSale.ObtenerHotSalesFuturos();
            var hotSale = hotSalesActuales.Where(t => t.IdHotSale == idHotSale).SingleOrDefault();

            ReservaViewModel nuevaReserva = new ReservaViewModel();
            nuevaReserva.IdCliente = sesionUser.IdCliente;
            nuevaReserva.IdPropiedad = hotSale.IdPropiedad;
            nuevaReserva.FechaReserva = hotSale.FechaDisponible;
            nuevaReserva.Credito = false;

            var mensaje = this.reservaService.AgregarReservaDesdeHotSale(nuevaReserva);

            if (mensaje != "OK")
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
            else {
                this.servicioHotSale.OcuparHotSale(idHotSale);
                mensaje = string.Format("Se ha efectuado satisfactoriamente la reserva de la residencia");
            }            
                               
            return Json(mensaje, JsonRequestBehavior.AllowGet);    
        }
    }
}