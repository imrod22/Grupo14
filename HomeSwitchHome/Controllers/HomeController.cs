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
            return View(this.subastaService.ObtenerSubastas());
        }

        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.AllowAnonymous]
        public ActionResult Login(string usuario, string password)
        {
            var usuarioActual = this.usuarioService.ObtenerUsuarioRegistrado(usuario, password);

            if (usuarioActual != null)
            {
                if (this.usuarioService.EsAdmin(usuarioActual.IdUsuario))
                {
                    FormsAuthentication.SetAuthCookie("ADMIN", true);
                    return RedirectToAction("Index", "Admin", new { });

                }
                else {

                    var clienteActual = this.usuarioService.ObtenerInformacionCliente(usuarioActual.IdUsuario);                    
                    var rol = "CLIENTE";

                    if (this.usuarioService.EsUsuarioPremium(usuarioActual.IdUsuario))
                    {
                        clienteActual.Premium = this.usuarioService.ObtenerInformacionPremium(clienteActual.IdCliente);
                        rol = "PREMIUM";
                    }

                    Session.Add("ClienteActual", clienteActual);

                    FormsAuthentication.SetAuthCookie(rol, true);
                    return Json(Url.Action("Index", "Home"));
                }
            }
             
            return null;
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
            var seRegistro = this.usuarioService.RegistrarNuevoCliente(nuevoUsuario);

            if (seRegistro)
                return Json(Url.Action("Index", "Home"));

            else return Json(null);
        }

    }
}