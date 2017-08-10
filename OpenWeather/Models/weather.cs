using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace OpenWeather.Models
{
    public class weather
    {
        public int id;

        public string main;

        public string description;

        private string icon;

        public Bitmap Icon
        {
            get
            {
                return new Bitmap(Image.FromFile($"icons/{icon}.png"));
            }
        }
    }
}