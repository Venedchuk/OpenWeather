using System.Web.Mvc;
using OpenWeather.Models;

namespace OpenWeather.Controllers
{
    public class HomeController : Controller
    {
        WorkWithDatabase parser = new WorkWithDatabase();
        public ActionResult Index(string City)
        {
            if (City == "")
                City = "Kyiv";

            parser.FindCityId(City);

            return View();
        }
        public ActionResult Parse()
        {
           
            parser.LetsParse();

            return View();
        }

    }
}