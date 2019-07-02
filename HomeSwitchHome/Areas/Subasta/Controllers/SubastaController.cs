using HomeSwitchHome.Services;
using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Subasta.Controllers
{
    public class SubastaController : Controller
    {
        readonly ISubastaService servicioSubasta;
        readonly IPujaService pujaService;

        public SubastaController(ISubastaService subastaServicio, IPujaService pujaService)
        {
            this.servicioSubasta = subastaServicio;
            this.pujaService = pujaService;
        }

        // GET: Subasta/Subasta
        public ActionResult Index()
        {   
            return View();
        }

        public JsonResult Subastas()
        {
            var subastasActivas = this.servicioSubasta.ObtenerSubastasActivas();
            return Json(subastasActivas.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult PujarEnSubasta(string idSubasta, string valorPujado)
        {
            SUBASTA subastaPujada = new SUBASTA();
            subastaPujada.ValorActual = Convert.ToDecimal(valorPujado);
            var sesionUser = (ClienteViewModel)Session["ClienteActual"];

            var mensaje = this.servicioSubasta.PujarSubasta(subastaPujada, int.Parse(idSubasta), sesionUser.IdCliente);

            if (mensaje == "OK")
            {
                var activas = this.servicioSubasta.ObtenerSubastasActivas();
                var subastaActual = activas.Where(t => t.IdSubasta == int.Parse(idSubasta)).SingleOrDefault();

                this.pujaService.RegistrarPuja(subastaActual.IdSubasta, sesionUser.IdCliente, subastaActual.ValorActual);

                return Json(this.servicioSubasta.ObtenerSubastasActivas().ToArray(), JsonRequestBehavior.AllowGet);
            }                

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerInformacionSubasta(int idSubasta)
        {
            var actuales = this.servicioSubasta.ObtenerSubastasActivas();
            var subastaActual = actuales.Where(t => t.IdSubasta == idSubasta).SingleOrDefault();

            return Json(subastaActual, JsonRequestBehavior.AllowGet);
        }
    }
}