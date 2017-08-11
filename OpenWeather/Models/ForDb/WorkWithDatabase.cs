using Newtonsoft.Json;
using OpenWeather.Models.ForDb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OpenWeather.Models
{
    public class WorkWithDatabase
    {


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

        internal void FindCityId(string city)
        {
            using (ConnectToDb db = new ConnectToDb())
            {


            }
            }
    }
}