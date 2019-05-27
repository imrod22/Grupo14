using System.Web.Mvc;
using HomeSwitchHome.Services;

namespace HomeSwitchHome.Controllers
{
    public class HomeController : Controller
    {

        readonly ISubastaService subastaService;

        public HomeController(ISubastaService subastaServicio)
        {
            this.subastaService = subastaServicio;
        }

        public ActionResult Index()
        {
            return View(this.subastaService.ObtenerSubastas());
        }

        public ActionResult Propiedades()
        {
            return View();
        }

        //public ActionResult Subastas()
        //{
        //    var arraySubastas = subastaController.GetSubastas();
        //    return View(arraySubastas);
        //}

        public ActionResult CrearPropiedad()
        {
            return View();
        }

        //public ActionResult ModificarPropiedad(string idPropiedad)
        //{
        //    var arrayPropiedades = propiedadController.GetPropiedades();

        //    var propAModificar = arrayPropiedades.Where(t => t.IdPropiedad == Convert.ToInt32(idPropiedad)).FirstOrDefault();
                       
        //    return View(propAModificar);
        //}

        public ActionResult CrearSubasta()
        {
            return View();
        }

        //public JsonResult GetPropiedades()
        //{
        //    var arrayPropiedades = propiedadController.GetPropiedades();

        //    return Json(arrayPropiedades.ToArray(), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult GetSubastas()
        //{
        //    var arraySubastas = subastaController.GetSubastas();

        //    return Json(arraySubastas.ToArray(), JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult AddPropiedad(string nombre, string domicilio, string descripcion, string pais)
        //{
        //    PROPIEDAD nuevaResidencia = new PROPIEDAD();
        //    nuevaResidencia.Nombre = nombre;
        //    nuevaResidencia.Pais = pais;
        //    nuevaResidencia.Descripcion = descripcion;
        //    nuevaResidencia.Domicilio = domicilio;

        //    return Json(propiedadController.SavePropiedad(nuevaResidencia));
        //}

        //public JsonResult ModificarPropiedad(string nombre, string domicilio, string descripcion, string pais)
        //{
        //    PROPIEDAD nuevaResidencia = new PROPIEDAD();
        //    nuevaResidencia.Nombre = nombre;
        //    nuevaResidencia.Pais = pais;
        //    nuevaResidencia.Descripcion = descripcion;
        //    nuevaResidencia.Domicilio = domicilio;

        //    return Json(propiedadController.SavePropiedad(nuevaResidencia));
        //}

        //public JsonResult AddSubasta(string idPropiedad, string valorMinimo, string fechaComienzo)
        //{
        //    SUBASTA nuevaSubasta = new SUBASTA();
        //    nuevaSubasta.IdPropiedad =  Convert.ToInt32(idPropiedad);
        //    nuevaSubasta.ValorMinimo = Convert.ToDecimal(valorMinimo);
        //    nuevaSubasta.FechaComienzo = Convert.ToDateTime(fechaComienzo);

        //    return Json(subastaController.SaveSubasta(nuevaSubasta));

        //}

        //public JsonResult NuevaPuja(string idSubasta, string valorActual)
        //{
        //    SUBASTA nuevaPuja = new SUBASTA();
        //    nuevaPuja.IdPropiedad = Convert.ToInt32(idSubasta);
        //    nuevaPuja.ValorActual = Convert.ToDecimal(valorActual);

        //    return Json(subastaController.PujarSubasta(nuevaPuja));
        //}
    }
}