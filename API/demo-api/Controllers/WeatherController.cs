// Controllers/ProductsController.cs
using Microsoft.AspNetCore.Mvc;
using DemoAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace DemoAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        [HttpGet]
        public ActionResult<WeatherForecast> GetWeather()
        {
            return Ok(new WeatherForecast { Id = 1, Name = "Sunny", Issued = DateTime.Now, TemperatureF = 75 });
        }
    }
}
