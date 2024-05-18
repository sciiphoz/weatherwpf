using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherwpf.Data
{
    public class Weather
    {
        public Weather(DateTime date, int temp, WeatherStatus status) 
        {
            ID = Guid.NewGuid().ToString();
            dateTime = date;
            Temp = temp;
            weatherStatus = status;
        }
        public string ID { get; set; }
        public DateTime dateTime { get; set; }
        public int Temp { get; set; }

        public WeatherStatus? weatherStatus { get; set; }
    }
}
