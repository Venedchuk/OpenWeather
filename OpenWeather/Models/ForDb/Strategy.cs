using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenWeather.Models.ForDb
{
    public interface IWearingStrategy
    {
        string GetUrlIcon();
    }
    public class StrategyWeatherIcon
    {
        private IWearingStrategy _wearingStrategy = new DefaultWearningStrategy();

        public void ChangeStrategy(IWearingStrategy wearingStrategy)
        {
            _wearingStrategy = wearingStrategy;
        }
    }

    internal class DefaultWearningStrategy : IWearingStrategy
    {
        public string GetUrlIcon()
        {
            return "http://openweathermap.org/img/w/50d.png";
        }
    }

    class SunshineStrategy : IWearingStrategy
    {
        public string GetUrlIcon()
        {
            return "http://openweathermap.org/img/w/01d.png";
        }
    }
    class RainStrategy : IWearingStrategy
    {
        public string GetUrlIcon()
        {
            return "http://openweathermap.org/img/w/09d.png";
        }
    }
    class CloudsStrategy : IWearingStrategy
    {
        public string GetUrlIcon()
        {
            return "http://openweathermap.org/img/w/04d.png";
        }
    }


}