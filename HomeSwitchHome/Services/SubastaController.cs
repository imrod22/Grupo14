using HomeSwitchHome.ViewModels;
using LazyCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeSwitchHome.Services
{
    public class SubastaController : Controller
    {
        private HomeSwitchHomeDB HomeSwitchDB;
        IAppCache HomeSwitchCache = new CachingService();

        public SubastaController()
        {
            this.HomeSwitchDB = new HomeSwitchHomeDB();
        }


        public List<SubastaViewModel> GetSubastas()
            {
                List<SubastaViewModel> subastasToShow = (List<SubastaViewModel>)HomeSwitchCache.CacheProvider.Get("subastas");

                if (subastasToShow == null)
                {
                    subastasToShow = new List<SubastaViewModel>();
                    var subastaDB = this.HomeSwitchDB.SUBASTA.ToList();

                    foreach (var subasta in subastaDB)
                    {
                        subastasToShow.Add(new SubastaViewModel().ToViewModel(subasta));
                    }

                    HomeSwitchCache.CacheProvider.Set("subastas", subastasToShow, null);
                }

                return subastasToShow;
            }
        
    }
}