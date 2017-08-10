using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenWeather.Models
{
    public class CityForSearch
    {
        //[Key]
        public int id{ get; set; }

        public string name { get; set; }

        public string country { get; set; }

        public Coord coord { get; set; }
    }
}