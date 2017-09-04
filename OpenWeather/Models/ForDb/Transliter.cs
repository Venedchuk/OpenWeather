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
                words = new Dictionary<string, string>();
                words.Add("а", "a");
                words.Add("б", "b");
                words.Add("в", "v");
                words.Add("г", "g");
                words.Add("ґ", "g");
                words.Add("д", "d");
                words.Add("е", "e");
                words.Add("є", "е");
                words.Add("ж", "zh");
                words.Add("з", "z");
                words.Add("и", "y");
                words.Add("і", "i");
                words.Add("ї", "i");
                words.Add("й", "j");
                words.Add("к", "k");
                words.Add("л", "l");
                words.Add("м", "m");
                words.Add("н", "n");
                words.Add("о", "o");
                words.Add("п", "p");
                words.Add("р", "r");
                words.Add("с", "s");
                words.Add("т", "t");
                words.Add("у", "u");
                words.Add("ф", "f");
                words.Add("х", "h");
                words.Add("ц", "c");
                words.Add("ч", "ch");
                words.Add("ш", "sh");
                words.Add("щ", "sch");
                words.Add("ь", "");
                words.Add("ю", "yu");
                words.Add("я", "ya");
                words.Add("А", "A");
                words.Add("Б", "B");
                words.Add("В", "V");
                words.Add("Г", "G");
                words.Add("Ґ", "G");
                words.Add("Д", "D");
                words.Add("Е", "T");
                words.Add("Є", "R");
                words.Add("Ж", "Zh");
                words.Add("З", "Z");
                words.Add("І", "I");
                words.Add("Ї", "I");
                words.Add("Й", "J");
                words.Add("К", "K");
                words.Add("Л", "L");
                words.Add("М", "M");
                words.Add("Н", "N");
                words.Add("О", "O");
                words.Add("П", "P");
                words.Add("Р", "R");
                words.Add("С", "S");
                words.Add("Т", "T");
                words.Add("У", "U");
                words.Add("Ф", "F");
                words.Add("Х", "H");
                words.Add("Ц", "C");
                words.Add("Ч", "Ch");
                words.Add("Ш", "Sh");
                words.Add("Щ", "Sch");
                words.Add("Ь", "J");
                words.Add("Ю", "Yu");
                words.Add("Я", "Ya");
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
            WeathData.weather[0].icon = GetIcon(WeathData.weather[0].description);


            return WeathData;
        }

        private string GetIcon(string description)//strategy
        {
            List<string> subStrings = new List<string> { "rain", "clouds", "clear", "rain" };
            string url = "http://openweathermap.org/img/w/";

            switch (subStrings.FirstOrDefault(description.Contains))
            {
                case "rain":
                    url += "09";
                    break;
                case "clouds":
                    url += "04";
                    break;
                case "clear":
                    url += "01";
                    break;
                default:
                    url += "50";
                    break;
            }

            return url += "d.png";
        }
    }


}