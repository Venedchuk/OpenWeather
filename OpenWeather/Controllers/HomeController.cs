using System.Web.Mvc;
using OpenWeather.Models;

namespace OpenWeather.Controllers
{
    public class HomeController : Controller
    {
        WorkWithDatabase parser = new WorkWithDatabase();
        public ActionResult Index(string City)
        {
            if (City == ""||City==null)
                City = "Zhytomyr";

       var WeatherData = parser.FindCityId(City);

            return View(WeatherData);
        }
        public ActionResult Parse()
        {
           
            parser.LetsParse();

            return View();
        }

    }
}