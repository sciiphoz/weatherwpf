using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weatherwpf.Data
{
    public static class DataContext
    {
        public static List<WeatherStatus> Statuses = new List<WeatherStatus>()
        {
            new WeatherStatus("Облачно"),
            new WeatherStatus("Ясно"),
            new WeatherStatus("Пасмурно"),

            new WeatherStatus("По умолчанию"),
        };

        public static List<SortType> SortTypes = new List<SortType>()
        {
            new SortType("По умолчанию"),
            new SortType("По возрастанию даты"),
            new SortType("По убыванию даты"),
            new SortType("По возрастанию температуры"),
            new SortType("По убыванию температуры"),
        };

        public static List<Weather> Days = new List<Weather>()
        {
            new Weather("15.05.2022", +14, Statuses[0]),
            new Weather("16.05.2022", -2, Statuses[2]),
            new Weather("17.05.2022", +24, Statuses[1]),
            new Weather("18.05.2022", +14, Statuses[1]),
            new Weather("19.05.2022", +12, Statuses[0]),
            new Weather("20.05.2022", +14, Statuses[2])
        };
    }
}
