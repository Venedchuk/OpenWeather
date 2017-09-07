using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace OpenWeather.Models.ForDb
{



    public class Transliter
    {
        private static Dictionary<string, string> words;//Singletone
        private Transliter()
        {
        }
        private static Transliter _transliter = null;
        public static Transliter GetInstance()
        {
            if (_transliter == null)
            {
                words = new Dictionary<string, string>()
                {
               {"а", "a" },
               {"б", "b"},
               {"в", "v"},
               {"г", "g"},
               {"ґ", "g"},
               {"д", "d"},
               {"е", "e"},
               {"є", "е"},
               {"ж", "zh"},
               {"з", "z"},
               {"и", "y"},
               {"і", "i"},
               {"ї", "i"},
               {"й", "j"},
               {"к", "k"},
               {"л", "l"},
               {"м", "m"},
               {"н", "n"},
               {"о", "o"},
               {"п", "p"},
               {"р", "r"},
               {"с", "s"},
               {"т", "t"},
               {"у", "u"},
               {"ф", "f"},
               {"х", "h"},
               {"ц", "c"},
               {"ч", "ch"},
               {"ш", "sh"},
               {"щ", "sch"},
               {"ь", ""},
               {"ю", "yu"},
               {"я", "ya"},
               {"А", "A"},
               {"Б", "B"},
               {"В", "V"},
               {"Г", "G"},
               {"Ґ", "G"},
               {"Д", "D"},
               {"Е", "T"},
               {"Є", "R"},
               {"Ж", "Zh"},
               {"З", "Z"},
               {"І", "I"},
               {"Ї", "I"},
               {"Й", "J"},
               {"К", "K"},
               {"Л", "L"},
               {"М", "M"},
               {"Н", "N"},
               {"О", "O"},
               {"П", "P"},
               {"Р", "R"},
               {"С", "S"},
               {"Т", "T"},
               {"У", "U"},
               {"Ф", "F"},
               {"Х", "H"},
               {"Ц", "C"},
               {"Ч", "Ch"},
               {"Ш", "Sh"},
               {"Щ", "Sch"},
               {"Ь", "J"},
               {"Ю", "Yu"},
               {"Я", "Ya"}
                };
                _transliter = new Transliter();
            }

            return _transliter;
        }


        public string GetTranslit(string sourceText)
        {

            string source = sourceText;
            foreach (KeyValuePair<string, string> pair in words)
            {

                source = source.Replace(pair.Key, pair.Value);
            }
            return source;
        }

        private DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public WeatherData Interpretate(WeatherData WeathData)//Інтерпретер
        {
            WeathData.sys.SunRise = UnixTimeStampToDateTime(WeathData.sys.sunrise);
            WeathData.sys.SunSet = UnixTimeStampToDateTime(WeathData.sys.sunset);
            WeathData.main.temp = WeathData.main.temp - 273.15;
            WeathData.weather[0].icon = GetIcon(WeathData.weather[0].description).GetUrlIcon();


            return WeathData;
        }

        private IWearingStrategy GetIcon(string description)//strategy
        {
            List<string> subStrings = new List<string> { "rain", "clouds", "clear", "rain" };


            switch (subStrings.FirstOrDefault(description.Contains))
            {
                case "rain":
                    return new RainStrategy();
                case "clouds":
                    return new CloudsStrategy();
                case "clear":
                    return new SunshineStrategy();
                default:
                    return new DefaultWearningStrategy();
            }

        }
    }


}