using System.Web.Mvc;

namespace HomeSwitchHome.Areas.Propiedad
{
    public class PropiedadAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Propiedad";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Propiedad_default",
                "Propiedad/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}