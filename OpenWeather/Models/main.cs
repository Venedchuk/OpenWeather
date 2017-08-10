using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenWeather.Models
{
    public class main
    {
        private double _temp;

        public double temp
        {
            get
            {
                return _temp;
            }
            set
            {
                _temp = value - 273.15; //Celsium
            }
        }

        private double _preassure;

        public double preassure
        {

            get
            {
                return _preassure;
            }
            set
            {
                _preassure = value /1.33322; //pascali
            }
        }

        private double _tempmin;

        public double tempmin
        {
            get
            {
                return _tempmin;
            }
            set
            {
                _tempmin = value - 273.15; //Celsium
            }
        }
        private double _tempmax;

        public double tempmax
        {
            get
            {
                return _tempmax;
            }
            set
            {
                _tempmax = value - 273.15; //Celsium
            }
        }


    }
}