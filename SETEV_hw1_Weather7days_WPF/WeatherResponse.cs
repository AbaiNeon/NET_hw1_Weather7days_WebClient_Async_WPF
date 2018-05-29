using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SETEV_hw1_Weather7days_WPF
{
    public class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }
        public string Name { get; set; }
        public WeatherInfo[] Weather { get; set; }
    }
}
