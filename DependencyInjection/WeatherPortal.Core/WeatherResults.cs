using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherPortal.Core
{
    public class WeatherResults
    {
        public int code { get; set; }
        public string source { get; set; }
        public Result[] result { get; set; }
    }

    public class Result
    {
        public Station_Details station_details { get; set; }
        public Station_Readings[] station_readings { get; set; }
    }

    public class Station_Details
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public int distance { get; set; }
        public int synop_no { get; set; }
        public string station_name { get; set; }
    }

    public class Station_Readings
    {
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime datetime { get; set; }
        public float temperature { get; set; }
        public int humidity { get; set; }
        public float pressure { get; set; }
        public int wind_direction { get; set; }
        public float wind_speed { get; set; }
        public float last_hours_rainfall { get; set; }
    }
}
