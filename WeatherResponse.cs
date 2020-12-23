using System;
using System.Collections.Generic;
using System.Text;

namespace weather
{
    class WeatherResponse
    {
        public TemperatureInfo Main { get; set; }
        public string Name { get; set; }
    }
}
