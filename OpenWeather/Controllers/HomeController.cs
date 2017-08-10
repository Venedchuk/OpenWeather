using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace OpenWeather.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string carts)
        {


            return View();
        }


    }
}