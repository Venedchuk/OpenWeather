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

            //var WeatherData = parser.FindCityId(City);
            var WeatherData = parser.FindCity(City);


            return View(WeatherData);
        }

        public ActionResult GetWeatherData(string City)
        {

            var WeatherData = parser.FindCity(City);
            return PartialView(WeatherData);
        }


        public ActionResult Parse()
        {
           
            parser.LetsParse();

            return View();
        }

    }
}