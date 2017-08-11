using Newtonsoft.Json;
using OpenWeather.Models.ForDb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace OpenWeather.Models
{
    public class WorkWithDatabase
    {

        private string appid = "29865b431a4dd31cc1d4eb8df4d6859c";
        public void LetsParse()
        {

            //CityForSearch movie1 = JsonConvert.DeserializeObject<CityForSearch>(File.ReadAllText(@"D:\Projects\OpenWeather\OpenWeather\Content\city.list.min.json"));

            using (StreamReader file = File.OpenText(@"D:\Projects\OpenWeather\OpenWeather\Content\city.list.min.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                var cities = (CityForSearch[])serializer.Deserialize(file, typeof(CityForSearch[]));
                using (ConnectToDb db = new ConnectToDb())
                {
                    var cityUa = cities.Where(x => x.country == "UA");
                    foreach (var item in cityUa)
                    {
                        {
                            db.Cities.Add(item);
                            db.SaveChanges();
                        }
                    }
                    db.SaveChanges();

                }
            }
        }

        internal WeatherData FindCityId(string city)
        {
            CityForSearch result;
            using (ConnectToDb db = new ConnectToDb())
            {
               result = db.Cities.Single(x => x.name == city);
            }
            return GetResponseFromWeather(result);
        }



        private WeatherData GetResponseFromWeather(CityForSearch result)
        {
            WebRequest request = WebRequest.Create("api.openweathermap.org/data/2.5/weather?id=" + result.id + "&appid="+appid);

            request.Method = "POST";

            request.ContentType = "application/x-www-urlencoded";

            WebResponse response = request.GetResponse();

            string answer;
            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = reader.ReadToEnd();
                }
            }
            response.Close();

           return JsonConvert.DeserializeObject<WeatherData>(answer);
        }
    }
}