using HomeSwitchHome.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace HomeSwitchHome
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<ISubastaService, SubastaService>();
            container.RegisterType<IPropiedadService, PropiedadService>();
            container.RegisterType<IUsuarioService, UsuarioService>();
            container.RegisterType<IReservaService, ReservaService>();
            container.RegisterType<IPujaService, PujaService>();
            container.RegisterType<IMailService, MailService>();
            container.RegisterType<ICreditoService, CreditoService>();
            container.RegisterType<IHotsaleService, HotsaleService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}