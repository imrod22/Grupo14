using HomeSwitchHome.Services;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;

namespace HomeSwitchHome
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<ISubastaService, SubastaService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}