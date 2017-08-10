using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.IO;
using OpenWeather.Models;

namespace OpenWeather.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string carts)
        {


            return View();
        }
        public ActionResult Parse(string carts)
        {
            ConvertToDatabase parser = new ConvertToDatabase();
            parser.LetsParse();

            return View();
        }

    }
}