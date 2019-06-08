using HomeSwitchHome.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Administrador.Controllers
{
    public class AdministradorController : Controller
    {
        readonly IUsuarioService servicioUsuario;
        readonly ISubastaService servicioSubasta;
        readonly IPropiedadService servicioPropiedad;

        public AdministradorController(IPropiedadService propiedadService, ISubastaService subastaService, IUsuarioService usuarioService)
        {
            this.servicioPropiedad = propiedadService;
            this.servicioSubasta = subastaService;
            this.servicioUsuario = usuarioService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Propiedades()
        {
            return Json(this.servicioPropiedad.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubastasSinEmpezar()
        {
            return Json(this.servicioSubasta.ObtenerSubastasFuturas().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SubastasCerradas()
        {
            return Json(this.servicioSubasta.ObtenerSubastasFinalizadas().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult NuevosClientes()
        {
            return Json(this.servicioUsuario.ObtenerNuevosClientes().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClientesAPremium()
        {
            return Json(this.servicioUsuario.ObtenerSolicitudesPremium().ToArray(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ModificarPropiedad(string idpropiedad, string descripcion, string pais)
        {
            PROPIEDAD nuevaResidencia = new PROPIEDAD();

            nuevaResidencia.Descripcion = descripcion;
            nuevaResidencia.Pais = pais;

            if (this.servicioPropiedad.ActualizarPropiedad(nuevaResidencia, Int32.Parse(idpropiedad)))
                return Json(this.servicioPropiedad.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult BorrarPropiedad(string idpropiedad)
        {
            if (this.servicioPropiedad.RemoverPropiedad(Int32.Parse(idpropiedad)))
                return Json(this.servicioPropiedad.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);

            return null;

        }

        public JsonResult CrearPropiedad(string nombre, string domicilio, string descripcion, string pais)
        {
            PROPIEDAD nuevaResidencia = new PROPIEDAD();
            nuevaResidencia.Nombre = nombre;
            nuevaResidencia.Pais = pais;
            nuevaResidencia.Descripcion = descripcion;

            if (this.servicioPropiedad.CrearPropiedad(nuevaResidencia))
                return Json(this.servicioPropiedad.ObtenerPropiedades().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult ObtenerInformacionPropiedad(int idPropiedad)
        {
            var propiedadesActuales = this.servicioPropiedad.ObtenerPropiedades();
            var currentPropiedad = propiedadesActuales.Where(t => t.IdPropiedad == idPropiedad).SingleOrDefault();
            return Json(currentPropiedad, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CrearSubasta(string propiedad, string fechaComienzo, string valorMinimo)
        {
            SUBASTA nuevaSubasta = new SUBASTA();
            nuevaSubasta.IdPropiedad = Int32.Parse(propiedad);
            nuevaSubasta.FechaComienzo = DateTime.Parse(fechaComienzo);
            nuevaSubasta.ValorMinimo = Convert.ToDecimal(valorMinimo);

            if (this.servicioSubasta.CrearSubasta(nuevaSubasta))
                return Json(this.servicioSubasta.ObtenerSubastasFuturas().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult ModificarSubasta(string idSubasta, string fechaComienzo, string valorMinimo)
        {
            SUBASTA subastaActualizada = new SUBASTA();
            subastaActualizada.FechaComienzo = DateTime.Parse(fechaComienzo);
            subastaActualizada.ValorMinimo = Convert.ToDecimal(valorMinimo);

            if (this.servicioSubasta.ActualizarSubasta(subastaActualizada, Int32.Parse(idSubasta)))
                return Json(this.servicioSubasta.ObtenerSubastasFuturas().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult BorrarSubasta(string idSubasta)
        {
            if (this.servicioSubasta.RemoverSubasta(Int32.Parse(idSubasta)))
                return Json(this.servicioSubasta.ObtenerSubastasFuturas().ToArray(), JsonRequestBehavior.AllowGet);

            return null;
        }

        public JsonResult ObtenerInformacionSubasta(int idSubasta)
        {
            var subastasActuales = this.servicioSubasta.ObtenerSubastasFuturas();

            var currentPropiedad = subastasActuales.Where(t => t.IdSubasta == idSubasta).SingleOrDefault();

            return Json(currentPropiedad, JsonRequestBehavior.AllowGet);
        }
    }
}