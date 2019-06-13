using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using HomeSwitchHome.Services;
using HomeSwitchHome.ViewModels;

namespace HomeSwitchHome.Controllers
{
    public class HomeController : Controller
    {
        readonly ISubastaService subastaService;
        readonly IUsuarioService usuarioService;
        
        public HomeController(ISubastaService subastaServicio, IUsuarioService usuarioServicio)
        {
            this.subastaService = subastaServicio;
            this.usuarioService = usuarioServicio;
        }

        public ActionResult Index()
        {
            return View(this.subastaService.ObtenerSubastasFuturas());
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Login(string usuario, string password)
        {
            var usuarioActual = this.usuarioService.ObtenerUsuarioRegistrado(usuario, password);
            
            if (usuarioActual != null)
            {
                if (!usuarioActual.Login)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(string.Format("Su solicitud de registro esta siendo procesada, se le notificara cuando pueda acceder con su cuenta."), JsonRequestBehavior.AllowGet);
                }

                if (this.usuarioService.EsAdmin(usuarioActual.IdUsuario))
                {
                    FormsAuthentication.SetAuthCookie("ADMIN", true);
                    return Json(Url.Action("Index", "Administrador", new { area = "Administrador"}));
                                       
                }
                else {

                    var clienteActual = this.usuarioService.ObtenerInformacionCliente(usuarioActual.IdUsuario);                    
                    var rol = "CLIENTE";

                    if (this.usuarioService.EsUsuarioPremium(usuarioActual.IdUsuario))
                    {   
                        rol = "PREMIUM";
                    }

                    Session.Add("ClienteActual", clienteActual);

                    FormsAuthentication.SetAuthCookie(rol, true);

                    return Json(Url.Action("Index", "Home"));
                }                
            }

            var mensaje = string.Format("Se ha ingresado un nombre de usuario o password invalido.");
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(mensaje, JsonRequestBehavior.AllowGet);

        }

        [System.Web.Mvc.Authorize]
        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registro()
        {
            return this.View();
        }

        public ActionResult RegistrarUsuario([FromBody] ClienteViewModel nuevoUsuario)
        {
            var mensajeRegistro = this.usuarioService.RegistrarNuevoCliente(nuevoUsuario);

            if (mensajeRegistro.Equals("OK"))
                return Json(Url.Action("Index", "Home"));

            else {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(string.Format(mensajeRegistro), JsonRequestBehavior.AllowGet);
            }
        }
    }
}