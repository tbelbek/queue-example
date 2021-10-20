using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingWith_Queue
{
    public class WeatherDataGenerator : DataGenerator<WeatherForecast>, IWeatherDataGenerator
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public override WeatherForecast CreateSingleRandom()
        {
            return new WeatherForecast
            {
                Date = RandomDay(),
                TemperatureC = gen.Next(-20, 55),
                Summary = Summaries[gen.Next(Summaries.Length)]
            };
        }

        public override List<WeatherForecast> CreateBulkRandom()
        {
            ConcurrentBag<WeatherForecast> list = new ConcurrentBag<WeatherForecast>();
            Parallel.For(0, 500, toExclusive => list.Add(CreateSingleRandom()));
            return list.ToList();
        }

        private static DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(gen.Next(range));
        }
    }
}