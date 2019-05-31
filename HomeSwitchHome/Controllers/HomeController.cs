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
                    var rol = "CLIENTE";
                    if (this.usuarioService.EsUsuarioPremium(usuarioActual.IdUsuario))
                    {
                        rol = "PREMIUM";
                    }

                    FormsAuthentication.SetAuthCookie(rol, true);
                    return RedirectToAction("Index", "Admin");
                }

                
                
            }
             
            return null;
        }

        [System.Web.Mvc.Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registrarse([FromBody] ClienteViewModel nuevoUsuario)
        {



            return RedirectToAction("Index", "Home");
        }

    }
}