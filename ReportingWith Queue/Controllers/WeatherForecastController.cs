using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ReportingWith_Queue.Controllers
{
    [ApiController]
    [Route("weather-forecast")]
    public class WeatherForecastController : GenericController
    {
        private readonly IWeatherDataGenerator _weatherDataGenerator;

        public WeatherForecastController(IWeatherDataGenerator weatherDataGenerator)
        {
            _weatherDataGenerator = weatherDataGenerator;
        }

        [HttpGet]
        [Route("get-single")]
        public WeatherForecast GetSingle()
        {
            return _weatherDataGenerator.CreateSingleRandom();
        }

        [HttpGet]
        [Route("get-queue-type")]
        public string GetQueueType()
        {
            QueueAttribute queueAttribute =
                (QueueAttribute)Attribute.GetCustomAttribute(typeof(WeatherForecast), typeof(QueueAttribute));
            return queueAttribute != null ? queueAttribute.Name.ToString() : "Queue not found";
        }

        [HttpGet]
        [Route("get-bulk")]
        public List<WeatherForecast> GetBulk()
        {
            return _weatherDataGenerator.CreateBulkRandom();
        }
    }

    public class QueueResponse
    {
        public string QueueStatus { get; set; }
    }
}