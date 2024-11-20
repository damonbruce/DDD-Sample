using System.ComponentModel.DataAnnotations;

namespace DemoAPI.Models
{
    public class WeatherForecast
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Issued { get;set;}
        public int TemperatureF {get; set;}
    }
}