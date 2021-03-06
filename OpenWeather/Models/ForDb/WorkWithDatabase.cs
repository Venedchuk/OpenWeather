﻿using Newtonsoft.Json;
using OpenWeather.Models.ForDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;


namespace OpenWeather.Models
{
    public class WorkWithDatabase
    {

        private string appid = "29865b431a4dd31cc1d4eb8df4d6859c";
        public Transliter translate = Transliter.GetInstance();

        public void LetsParse()
        {
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
            if (city == null)
                city = "Zhytomyr";
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

            translate = Transliter.GetInstance();
            WeathData = translate.Interpretate(WeathData);



            return WeathData;
        }


        
        

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

            WeathData = translate.Interpretate(WeathData);
            

            return WeathData;
        }


    }
}