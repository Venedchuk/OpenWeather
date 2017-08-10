using Newtonsoft.Json;
using OpenWeather.Models.ForDb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OpenWeather.Models
{
    public class ConvertToDatabase
    {


        public void LetsParse()
        {

            //CityForSearch movie1 = JsonConvert.DeserializeObject<CityForSearch>(File.ReadAllText(@"D:\Projects\OpenWeather\OpenWeather\Content\city.list.min.json"));

            using (StreamReader file = File.OpenText(@"D:\Projects\OpenWeather\OpenWeather\Content\city.list.min.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                var cities = (CityForSearch [])serializer.Deserialize(file, typeof(CityForSearch []));
                using (ConnectToDb db = new ConnectToDb())
                {
                    foreach (var item in cities)
                    {
                        db.Cities.Add(item);
                    }
                    db.SaveChanges();

                }
            }
        }
    }
}