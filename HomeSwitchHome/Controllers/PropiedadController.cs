using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeSwitchHome;
using HomeSwitchHome.Services;

namespace HomeSwitchHome.Controllers
{
    public class PropiedadController : Controller
    {                       
        public ActionResult Index()
        {
            return View();
        }      
    }
}
