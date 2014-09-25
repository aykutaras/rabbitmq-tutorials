using System;
using System.Configuration;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQSamples.WorkQueues.Producer
{
    public class Sender
    {
        public static void Run(string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = ConfigurationManager.AppSettings["RabbitMQUrl"]
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("task_queue", true, false, false, null);
                    channel.BasicQos(0, 1, false);

                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();
                    properties.SetPersistent(true);
                    properties.DeliveryMode = 2;

                    channel.BasicPublish("", "task_queue", properties, body);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}
