using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherwpf.Data
{
    public class WeatherStatus
    {
        public WeatherStatus(string title)
        {
            Id = Guid.NewGuid().ToString();
            Title = title;
        }

        public string Id { get; set; }
        public string Title { get; set; }
    }
}
