using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace OpenWeather.Models
{
    public class WeatherData
    {
        public coord coord;

        public weather[] weather;

        [JsonProperty("base")]
        public string Base;

        public main main;

        public double visibility;

        public wind wind;

        public clouds clouds;

        public double dt;

        public sys sys;

        public int id;

        public string name;

        public int cod;


    }
}