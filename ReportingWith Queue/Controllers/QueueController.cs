using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace ReportingWith_Queue.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueueController : GenericController
    {
        [HttpGet]
        [Route("add-sample")]
        public QueueResponse AddSampleToQueue(string queueName)
        {
            var channel = QueueCreator(queueName);

            PublishToQueue(queueName, channel, "Hello");

            return new QueueResponse()
            {
                QueueStatus = "Done publishing."
            };
        }

        private IModel QueueCreator(string queueName)
        {
            var factory = new ConnectionFactory() { HostName = "belbek.server", Port = 5672 };

            using var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            return channel;
        }

        private static void PublishToQueue(string queueName, IModel channel, string queueObject)
        {
            var body = Encoding.UTF8.GetBytes(queueObject);

            channel.BasicPublish(exchange: "",
                routingKey: queueName,
                basicProperties: null,
                body: body);
        }

        [HttpPost]
        public QueueResponse AddToQueue(string objType, List<object> objList)
        {
            var channel = QueueCreator(objType);
            var publishList = new ConcurrentBag<object>(objList);

            Parallel.ForEach(publishList, t => PublishToQueue(objType, channel, JsonConvert.SerializeObject(t)));

            return new QueueResponse()
            {
                QueueStatus = "Done publishing."
            };
        }
    }
}