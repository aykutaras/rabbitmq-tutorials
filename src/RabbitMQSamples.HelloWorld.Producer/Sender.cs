using System;
using System.Configuration;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMQSamples.HelloWorld.Producer
{
    public class Sender
    {
        public static void Run()
        {
            var factory = new ConnectionFactory
            {
                HostName = ConfigurationManager.AppSettings["RabbitMQUrl"]
            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);

                    var message = "Hello World!";
                    var messageBytes = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish("", "hello", null, messageBytes);
                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }
        }
    }
}
