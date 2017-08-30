using Newtonsoft.Json;
using OpenWeather.Models.ForDb;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        } //create database with ua loc

        internal WeatherData FindCity(string city)
        {
            string url = "http://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + appid;

            WebRequest request = WebRequest.Create(url);
            Debug.Print(request.ToString());
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

            var WeathData = JsonConvert.DeserializeObject<WeatherData>(answer);

            WeathData.sys.SunRise = UnixTimeStampToDateTime(WeathData.sys.sunrise);
            WeathData.sys.SunSet = UnixTimeStampToDateTime(WeathData.sys.sunset);

            return WeathData;
        }

        public Transliter translate = new Transliter();
        

        internal WeatherData FindCityId(string city)
        {
            
            CityForSearch result;
            city = translate.GetTranslit(city);

            using (ConnectToDb db = new ConnectToDb())
            {
                result = db.Cities.SingleOrDefault(x => x.name == city);
                if (result == null)
                    return GetResponseFromWeather(db.Cities.SingleOrDefault(x => x.id == 696050));
            }

            return GetResponseFromWeather(result);
        }

       

        private WeatherData GetResponseFromWeather(CityForSearch result)
        {



            string url = "http://api.openweathermap.org/data/2.5/weather?id=" + result.id + "&appid=" + appid;

            WebRequest request = WebRequest.Create(url);
            Debug.Print(request.ToString());
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

            var WeathData = JsonConvert.DeserializeObject<WeatherData>(answer);

            WeathData.sys.SunRise = UnixTimeStampToDateTime(WeathData.sys.sunrise);
            WeathData.sys.SunSet = UnixTimeStampToDateTime(WeathData.sys.sunset);

            return WeathData;
        }

        public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}