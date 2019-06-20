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

        public SubastaController(ISubastaService subastaServicio)
        {
            this.servicioSubasta = subastaServicio;
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
            
            if(mensaje == "OK")            
                return Json(this.servicioSubasta.ObtenerSubastasActivas().ToArray(), JsonRequestBehavior.AllowGet);

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ObtenerInformacionSubasta(int idSubasta)
        {
            var subastasActuales = this.servicioSubasta.ObtenerSubastasActivas();
            var currentPropiedad = subastasActuales.Where(t => t.IdSubasta == idSubasta).SingleOrDefault();

            return Json(currentPropiedad, JsonRequestBehavior.AllowGet);
        }
    }
}